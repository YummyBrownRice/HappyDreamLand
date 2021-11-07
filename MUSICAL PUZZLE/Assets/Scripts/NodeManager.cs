using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeManager : MonoBehaviour
{
    #region Basic Node Logic
    public Node[] nodes;
    public List<GameObject> nodeKinds;
    public int soundSourceCount;

    private BeatManager beatManager;
    private GridManager gridManager;

    public enum nodeType
    {
        SoundSource,
        Link,
        Inverse
    }

    private void Awake()
    {
        beatManager = GameObject.Find("SoundManager").GetComponent<BeatManager>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
    }

    void Start()
    {
        UpdatePuzzle();
        AddNode(nodeKinds[(int)nodeType.Inverse], new Vector3(0, 0, 0), nodeType.Inverse);
        AddNode(nodeKinds[1], new Vector3(1, -2, 1), nodeType.Link);
        AddNode(nodeKinds[2], new Vector3(0, -1, 1), nodeType.Inverse);
        AddNode(nodeKinds[1], new Vector3(0, -4, 4), nodeType.Link);
        AddNode(nodeKinds[1], new Vector3(1, -1, 0), nodeType.Link);
        AddNode(nodeKinds[2], new Vector3(1, -3, 2), nodeType.Inverse);
        AddConnection(0, 2, 0);
        AddConnection(2, 3, 0);
        AddConnection(1, 3, 1);
        AddConnection(2, 4, 0);
        AddConnection(0, 5, 0);
        AddConnection(4, 5, 1);
        AddConnection(3, 6, 0);
        AddConnection(5, 6, 1);
        AddConnection(6, 7, 0);
        RemoveConnection(4, 5);
        RemoveConnection(0, 3); //Nonexistent connection -> error
        RemoveNode(6);
        RemoveNode(3);
    }

    public void UpdatePuzzle()
    {
        int index_ = 0;
        foreach (Transform node in transform)
        {
            Node n = node.GetComponent<Node>();
            nodes[index_] = n;
            n.index = index_;
            index_++;
        }

        UpdateBeat();
    }

    public bool ExtractOutput(int u, ref List<Sequence> newSequences)
    {
        if (nodes[u].inputCapacity != nodes[u].inputCount)
        {
            return false;
        }
        if (nodes[u].extracted)
        {
            return true;
        }
        bool flag = true;
        for (int i = 0; i < nodes[u].outputNodes.Count; i++)
        {
            flag = !ExtractOutput(nodes[u].outputNodes[i], ref newSequences) && flag;
        }
        if (flag && !nodes[u].extracted)
        {
            newSequences.Add(nodes[u].Output);
            nodes[u].extracted = true;
        }
        return true;
    }

    public void FeedInput(int u)
    {
        nodes[u].extracted = false;
        if (nodes[u].inputCapacity != nodes[u].inputCount)
        {
            return;
        }
        nodes[u].Process();
        for (int i = 0; i < nodes[u].outputNodes.Count; i++)
        {
            //nodes[nodes[u].outputNodes[i]].input.Add(nodes[u].Output);
            int inputIndex = Array.IndexOf(nodes[nodes[u].outputNodes[i]].inputNodes, u);

            nodes[nodes[u].outputNodes[i]].input[inputIndex] = nodes[u].Output;
            nodes[nodes[u].outputNodes[i]].inputCount += 1;
            FeedInput(nodes[u].outputNodes[i]);
        }
    }

    public void UpdateBeat()
    {
        List<Sequence> newSequences = new List<Sequence>();
        foreach (Node node in nodes)
        {
            if (node == null)
            {
                continue;
            }
            node.input = new Sequence[100];
            node.inputCount = 0;
        }

        for (int i = 0; i < soundSourceCount; i++)
        {
            FeedInput(i);
        }

        for (int i = 0; i < soundSourceCount; i++)
        {
            ExtractOutput(i, ref newSequences);
        }



        //Find terminal nodes for playing sounds

        beatManager.sequenceList = newSequences;
    }
    #endregion

    #region Node manipulation

    public void AddNode(GameObject obj, Vector3 coordinate, nodeType nodeType)
    {
        GameObject go = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity, transform);

        int index_ = Array.IndexOf(nodes, null);

        Node nodeComp = go.GetComponent<Node>();
        nodeComp.index = index_;
        nodeComp.nodeType = nodeType;
        nodes[index_] = nodeComp;
        gridManager.indexToGridcell[gridManager.indexToCoordinate.FindIndex(d => d == coordinate)].ConnectToNode(index_);
    }

    public void AddConnection(int i1, int i2, int inputIndex)
    {
        if (i1 == i2 || inputIndex >= nodes[i2].inputCapacity || nodes[i2].input[inputIndex] != null)
        {
            return;
        }
        nodes[i1].outputNodes.Add(i2);
        nodes[i2].inputNodes[inputIndex] = i1;
        UpdateBeat();
    }

    public void RemoveConnection(int i1, int i2)
    {
        if (i1 == i2 || nodes[i1] == null || nodes[i2] == null)
        {
            Debug.LogError("What node is this?");
            UpdateBeat();
            return;
        }
        for (int i = 0; i < nodes[i2].inputCapacity; i++)
        {
            if (nodes[i2].inputNodes[i] == i1)
            {
                nodes[i2].inputNodes[i] = -1;
                nodes[i1].outputNodes.Remove(i2);
                UpdateBeat();
                return;
            }
        }
        Debug.LogError("You tried to remove a nonexistent connection");
    }

    public void RemoveNode(int index)
    {
        if (nodes[index] == null)
        {
            Debug.LogError("You tried to remove a nonexistent node");
            return;
        }
        for (int i = 0; i < nodes[index].inputCapacity; i++)
        {
            if (nodes[index].inputNodes[i] != -1)
            {
                RemoveConnection(nodes[index].inputNodes[i], index);
            }
        }
        for (int i = 0; i < nodes[index].outputNodes.Count; i++)
        {
            if (nodes[index].outputNodes[i] != -1)
            {
                RemoveConnection(index, nodes[index].outputNodes[i]);
            }
        }
    }

    #endregion
}
