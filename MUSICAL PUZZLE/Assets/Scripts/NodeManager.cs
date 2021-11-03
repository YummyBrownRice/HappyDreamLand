using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    private PuzzleManager puzzleManager;
    void Awake()
    {
        puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        UpdatePuzzle();
    }

    public void UpdatePuzzle()
    {
        puzzleManager.nodes = new List<Node>();
        int i = 0;
        foreach (Transform node in transform)
        {
            Node nodeComp = node.GetComponent<Node>();
            puzzleManager.nodes.Add(nodeComp);
            nodeComp.index = i;
            i++;
        }
        puzzleManager.UpdateBeat();
    }
}
