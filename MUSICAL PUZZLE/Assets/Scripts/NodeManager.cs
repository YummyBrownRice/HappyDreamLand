using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeManager : MonoBehaviour
{
    #region Basic Node Logic
    public Node[] nodes;
    public List<GameObject> nodeKinds;
    private BeatManager beatManager;
    private void Awake()
    {
        beatManager = GameObject.Find("SoundManager").GetComponent<BeatManager>();
    }

    void Start()
    {
        UpdatePuzzle();
        AddNode(nodeKinds[1], new Vector2(1, 2));
        AddConnection(0, 2);
        AddConnection(1, 2);
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
        if (nodes[u].outputNodes.Count == 0 && nodes[u].inputCapacity == nodes[u].input.Count)
        {
            if (!nodes[u].extracted)
            {
                newSequences.Add(nodes[u].Output);
                nodes[u].extracted = true;
            }
            return true;
        }
        bool flag = nodes[u].inputCapacity == nodes[u].input.Count;
        //Debug.Log(nodes[u].outputNodes[0]);
        for (int i = 0; i < nodes[u].outputNodes.Count; i++)
        {
            flag = !ExtractOutput(nodes[u].outputNodes[i], ref newSequences) && flag;
        }
        if (flag && !nodes[u].extracted)
        {
            newSequences.Add(nodes[u].Output);
            nodes[u].extracted = true;
        }
        return flag;
    }

    public void FeedInput(int u)
    {
        nodes[u].extracted = false;
        if (nodes[u].inputCapacity != nodes[u].input.Count)
        {
            return;
        }
        nodes[u].Process();
        for (int i = 0; i < nodes[u].outputNodes.Count; i++)
        {
            nodes[nodes[u].outputNodes[i]].input.Add(nodes[u].Output);
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
            node.input.Clear();
        }
        FeedInput(0);
        FeedInput(1);
        ExtractOutput(0, ref newSequences);
        ExtractOutput(1, ref newSequences);


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

    public void AddConnection(int i1, int i2)
    {
        if (i1 == i2)
        {
            return;
        }
        nodes[i1].outputNodes.Add(i2);
        nodes[i2].inputNodes.Add(i1);
        UpdateBeat();
    }

    #endregion
}
