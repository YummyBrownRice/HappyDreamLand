using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node: MonoBehaviour
{

    public List<int> inputNodes;
    public int outputNode;
    public int index;

    protected List<Sequence> input;
    private Sequence output;
    private PuzzleManager puzzleManager;

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
        puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    }

    public virtual void Process()
    {
        Output = new Sequence(new Sequence.Beat[0]);
    }


}
