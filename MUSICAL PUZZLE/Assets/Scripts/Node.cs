using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Node : MonoBehaviour
{

    public int[] inputNodes;
    public int inputCapacity;
    public int inputCount;
    public List<int> outputNodes;
    public int index;

    public bool extracted;

    public Sequence[] input;

    public GridCell connectedCell;

    public NodeManager.nodeType nodeType;

    public Dictionary<string, object> args = new Dictionary<string, object>();

    public int rotation;

    public int[] inputDirections;
    public int[] outputDirections;

    private Sequence output;
    private NodeManager nodeManager;

    public Sequence Output
    {
        get
        {
            return output;
        }
        protected set
        {
            output = value;
        }
    }

    private void Awake()
    {
        inputNodes = new int[6];
        input = new Sequence[6];
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
    }

    public virtual void ProcessArguments(Dictionary<string, object> args)
    {

    }

    public virtual void Process()
    {
        Output = new Sequence(new Sequence.Beat[0]);
    }

    public int inputDirToIndex(int inputDir)
    {
        int[] originalInputDirections = new int[inputDirections.Length];
        for (int i = 0; i < inputDirections.Length; i++)
        {
            originalInputDirections[i] = (inputDirections[i] - rotation + 6) % 6;
        }
        Array.Sort(originalInputDirections);
        return Array.FindIndex(originalInputDirections, d => d == (inputDir - rotation + 6) % 6);
    }
}
