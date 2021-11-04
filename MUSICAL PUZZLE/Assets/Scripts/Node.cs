using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public List<int> inputNodes;
    public int inputCapacity;
    public List<int> outputNodes;
    public int index;

    public bool extracted;

    public List<Sequence> input;
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

    private void Start()
    {
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
    }

    public virtual void Process()
    {
        Output = new Sequence(new Sequence.Beat[0]);
    }


}
