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
    private void Awake()
    {
        beatManager = GameObject.Find("SoundManager").GetComponent<BeatManager>();
    }

    void Start()
    {
        UpdatePuzzle();
        AddNode(nodeKinds[1], new Vector2(-1, -2));
        AddNode(nodeKinds[2], new Vector2(3, 3));
        AddConnection(0, 3, 0);
        AddConnection(3, 2, 0);
        AddConnection(1, 2, 1);
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

    public bool ExtractOutput(int u, ref List<Sequence> newSequences) //BeatManager로 보낼 최종 sequence list를 추출
    {
        if (nodes[u].outputNodes.Count == 0 && nodes[u].inputCapacity == nodes[u].inputCount)
        {
            if (!nodes[u].extracted)
            {
                newSequences.Add(nodes[u].Output);
                Debug.Log("Hm,m");
                Debug.Log(u);
                nodes[u].extracted = true;
            }
            return true;
        }
        bool flag = nodes[u].inputCapacity == nodes[u].inputCount;
        //Debug.Log(nodes[u].outputNodes[0]);
        for (int i = 0; i < nodes[u].outputNodes.Count; i++)
        {
            flag = !ExtractOutput(nodes[u].outputNodes[i], ref newSequences) && flag;
        }
        if (flag && !nodes[u].extracted)
        {
            newSequences.Add(nodes[u].Output);
            Debug.Log("Hmmmmmmmm,m");
            Debug.Log(u);
            nodes[u].extracted = true;
        }
        return flag;
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
        Debug.Log("//////////////////////////////////////////////");
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

    public void AddNode(GameObject obj, Vector3 pos)
    {
        GameObject go = Instantiate(obj, pos, Quaternion.identity, transform);

        int index_ = Array.IndexOf(nodes, null);

        Node nodeComp = go.GetComponent<Node>();
        nodeComp.index = index_;
        nodes[index_] = nodeComp;
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

    #endregion
}
