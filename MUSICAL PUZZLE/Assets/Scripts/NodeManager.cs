using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public Node[] nodes;
    private BeatManager beatManager;
    private void Awake()
    {
        beatManager = GameObject.Find("SoundManager").GetComponent<BeatManager>();
    }

    void Start()
    {
        UpdatePuzzle();
    }

    public void UpdatePuzzle()
    {

        foreach (Transform node in transform)
        {
            Node n = node.GetComponent<Node>();
            nodes[n.index] = n;
        }

        UpdateBeat();
    }

    public bool dfs(int u, ref List<Sequence> newSequences)
    {
        if (nodes[u].outputCount == 0 && nodes[u].inputCount == nodes[u].inputNodes.Count)
        {
            nodes[u].Process();
            newSequences.Add(nodes[u].Output);
            return true;
        }
        bool flag = nodes[u].inputCount == nodes[u].inputNodes.Count;
        for (int i = 0; i < nodes[u].outputCount; i++)
        {
            flag = flag && !dfs(nodes[u].outputNode[i], ref newSequences);
        }
        if (flag)
        {
            nodes[u].Process();
            newSequences.Add(nodes[u].Output);
        }
        return flag;
    }

    public void UpdateBeat()
    {
        List<Sequence> newSequences = new List<Sequence>();

        dfs(0, ref newSequences);


        //Find terminal nodes for playing sounds

        beatManager.sequenceList = newSequences;
    }
}
