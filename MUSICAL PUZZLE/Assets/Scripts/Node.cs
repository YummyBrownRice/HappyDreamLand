using System.Collections;
using System.Collections.Generic;
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
        inputNodes = new int[100];
        input = new Sequence[100];
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
    }

    public virtual void ProcessArguments(Dictionary<string, object> args)
    {
        
    }

    public virtual void Process()
    {
        Output = new Sequence(new Sequence.Beat[0]);
    }


}
