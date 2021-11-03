using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<Node> nodes;
    private BeatManager beatManager;

    private void Awake()
    {
        beatManager = GameObject.Find("SoundManager").GetComponent<BeatManager>();
    }

    public void UpdateBeat()
    {
        List <Sequence> newSequences = new List<Sequence>();
        newSequences.Add(nodes[nodes.Count - 1].Output);

        beatManager.sequenceList = newSequences;
    }
}
