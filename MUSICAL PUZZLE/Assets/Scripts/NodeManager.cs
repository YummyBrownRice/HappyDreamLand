using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public List<Node> nodes;
    private BeatManager beatManager;
    private void Awake()
    {
        beatManager = GameObject.Find("SoundManager").GetComponent<BeatManager>();
    }

    void Start()
    {
        UpdatePuzzle();
    }

    public void UpdatePuzzle ()
    {
        List<Node> tempList = new List<Node>();
        foreach (Transform node in transform)
        {
            tempList.Add(node.GetComponent<Node>());
        }

        UpdateBeat();
    }
    public void UpdateBeat()
    {
        List<Sequence> newSequences = new List<Sequence>();

        //Find terminal nodes for playing sounds

        beatManager.sequenceList = newSequences;
    }
}
