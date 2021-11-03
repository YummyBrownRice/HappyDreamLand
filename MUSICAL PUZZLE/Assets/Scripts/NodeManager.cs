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

    public void UpdateBeat()
    {
        List<Sequence> newSequences = new List<Sequence>();

        int root = 0;


        //Find terminal nodes for playing sounds

        beatManager.sequenceList = newSequences;
    }
}
