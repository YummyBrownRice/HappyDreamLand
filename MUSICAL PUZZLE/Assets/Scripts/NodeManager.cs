using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeManager : MonoBehaviour
{
    #region Basic Node Logic


    public Node[] nodes;
    public bool[] isSource;
    public List<GameObject> nodeKinds;
    private BeatManager beatManager;
    private GridManager gridManager;
    private LevelManager levelManager;

    public enum nodeType
    {
        SoundSource,
        Link,
        Inverse,
        Add,
        Subtract,
        PadRight,
        PadLeft,
        DelayRight,
        DelayLeft,
        Split
    }

    private void Awake()
    {
        beatManager = GameObject.Find("SoundManager").GetComponent<BeatManager>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        levelManager = transform.GetComponent<LevelManager>();
        isSource = new bool[100];
    }

    void Start()
    {
        UpdatePuzzle();
        /*
        AddNode(nodeKinds[(int)nodeType.Inverse], new Vector3(0, 0, 0), nodeType.Inverse);
        AddNode(nodeKinds[1], new Vector3(1, -2, 1), nodeType.Link);
        AddNode(nodeKinds[2], new Vector3(0, -1, 1), nodeType.Inverse);
        AddNode(nodeKinds[1], new Vector3(0, -4, 4), nodeType.Link);
        AddNode(nodeKinds[1], new Vector3(1, -1, 0), nodeType.Link);
        AddNode(nodeKinds[2], new Vector3(1, -3, 2), nodeType.Inverse);
        AddConnection(0, 2, 0);
        AddConnection(2, 3, 0);
        AddConnection(1, 3, 1);
        AddConnection(2, 4, 0);
        AddConnection(0, 5, 0);
        AddConnection(4, 5, 1);
        AddConnection(3, 6, 0);
        AddConnection(5, 6, 1);
        AddConnection(6, 7, 0);
        RemoveConnection(4, 5);
        RemoveConnection(0, 3); //Nonexistent connection -> error
        RemoveNode(6);
        RemoveNode(3);
        */
        isSource[0] = true;
    }

    public void UpdatePuzzle()
    {
        LevelManager.Level level = levelManager.Load();
        /*
        int index_ = 0;
        foreach (Transform node in transform)
        {
            Node n = node.GetComponent<Node>();
            nodes[index_] = n;
            n.index = index_;
            index_++;
        }
        */

        //TODO: Instantiate Nodes & Connect w/ grid

        foreach (var cell in level.map)
        {
            Node nodeComp = AddNode(nodeKinds[(int)cell.type], cell.coordinate, cell.type, cell.rotation);
            if (cell.type == nodeType.SoundSource)
            {
                nodeComp.GetComponent<SoundSource>().sourceSound = cell.sourceSound;
            }
        }

        UpdateBeat();
    }

    public bool ExtractOutput(int u, ref List<Sequence> newSequences)
    {
        if (nodes[u].inputCapacity != nodes[u].inputCount)
        {
            return false;
        }
        if (nodes[u].extracted)
        {
            return true;
        }
        bool flag = true;
        for (int i = 0; i < nodes[u].outputNodes.Count; i++)
        {
            flag = !ExtractOutput(nodes[u].outputNodes[i], ref newSequences) && flag;
        }
        if (flag && !nodes[u].extracted)
        {
            newSequences.Add(nodes[u].Output);
            nodes[u].extracted = true;
        }
        return true;
    }

    public void FeedInput(int u)
    {
        nodes[u].extracted = false;
        if (nodes[u].inputCapacity != nodes[u].inputCount)
        {
            return;
        }
        nodes[u].Process();
        for (int i = 0; i < nodes[u].outputNodes.Count; i++)
        {
            //nodes[nodes[u].outputNodes[i]].input.Add(nodes[u].Output);
            int inputIndex = Array.IndexOf(nodes[nodes[u].outputNodes[i]].inputNodes, u);

            nodes[nodes[u].outputNodes[i]].input[inputIndex] = nodes[u].Output;
            nodes[nodes[u].outputNodes[i]].inputCount += 1;
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
            node.input = new Sequence[100];
            node.inputCount = 0;
        }

        for (int i = 0; i < 100; i++)
        {
            if (isSource[i])
            {
                FeedInput(i);
            }
        }

        for (int i = 0; i < 100; i++)
        {
            if (isSource[i])
            {
                ExtractOutput(i, ref newSequences);
            }
        }

        //Find terminal nodes for playing sounds

        beatManager.sequenceList = newSequences;
    }
    #endregion

    #region Node manipulation

    public Node AddNode(GameObject obj, Vector3 coordinate, nodeType nodeType, int rotation = 0)
    {
        GameObject go = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity, transform);

        int index_ = Array.IndexOf(nodes, null);

        Node nodeComp = go.GetComponent<Node>();
        nodeComp.index = index_;
        nodeComp.nodeType = nodeType;
        nodes[index_] = nodeComp;
        nodeComp.rotation = rotation;
        if (nodeType == (nodeType)0)
        {
            isSource[index_] = true;
        }
        int[] inputDirections = nodeComp.inputDirections;
        int[] outputDirections = nodeComp.outputDirections;

        for (int i = 0; i < inputDirections.Length; i++)
        {
            int num = (inputDirections[i] + rotation) % 6;
            inputDirections[i] = num;
        }
        for (int i = 0; i < outputDirections.Length; i++)
        {
            int num = (outputDirections[i] + rotation) % 6;
            outputDirections[i] = num;
        }

        gridManager.indexToGridcell[gridManager.indexToCoordinate.FindIndex(d => d == coordinate)].ConnectToNode(index_);

        foreach (var (i, d) in connectedCellsTo(index_))
        {
            Debug.Log("to");
            AddConnection(i, index_, d);
        }

        foreach (var (i, d) in connectedCellsFrom(index_))
        {
            Debug.Log("from");
            AddConnection(index_, i, d);
        }

        UpdateBeat();

        return nodeComp;
    }

    public void AddConnection(int i1, int i2, int inputIndex)
    {
        Debug.Log(inputIndex);
        if (i1 == i2 || nodes[i2].input[inputIndex] != null)
        {
            return;
        }
        nodes[i1].outputNodes.Add(i2);
        nodes[i2].inputNodes[inputIndex] = i1;
    }

    public void RemoveConnection(int i1, int i2)
    {
        if (i1 == i2 || nodes[i1] == null || nodes[i2] == null)
        {
            Debug.LogError("What node is this?");
            return;
        }
        for (int i = 0; i < nodes[i2].inputCapacity; i++)
        {
            if (nodes[i2].inputNodes[i] == i1)
            {
                nodes[i2].inputNodes[i] = -1;
                nodes[i1].outputNodes.Remove(i2);
                return;
            }
        }
        Debug.LogError("You tried to remove a nonexistent connection");
    }

    public void RemoveNode(int index)
    {
        if (nodes[index] == null)
        {
            Debug.LogError("You tried to remove a nonexistent node");
            return;
        }
        isSource[index] = false;
        for (int i = 0; i < nodes[index].inputCapacity; i++)
        {
            if (nodes[index].inputNodes[i] != -1)
            {
                RemoveConnection(nodes[index].inputNodes[i], index);
            }
        }
        for (int i = 0; i < nodes[index].outputNodes.Count; i++)
        {
            if (nodes[index].outputNodes[i] != -1)
            {
                RemoveConnection(index, nodes[index].outputNodes[i]);
            }
        }

        UpdateBeat();

        nodes[index].connectedCell.EmptyCell();

        Destroy(nodes[index].gameObject);
    }

    #endregion

    public System.Collections.Generic.IEnumerable<(int, int)> connectedCellsFrom(int index)
    {
        Vector3 centerPos = nodes[index].connectedCell.coordinate;
        //gridManager.indexToGridcell[gridManager.indexToCoordinate.FindIndex(d => d == coordinate)].ConnectToNode(index_);
        List<Vector3> dList = new List<Vector3> { new Vector3(0, -1, 1), new Vector3(1, -1, 0), new Vector3(1, 0, -1), new Vector3(0, 1, -1), new Vector3(-1, 1, 0), new Vector3(-1, 0, 1) };
        for (int i = 0; i < 100; i++)
        {
            if (nodes[i] != null)
            {
                Debug.Log(nodes[i].connectedCell.coordinate);
                int dir = dList.FindIndex(d => d == (nodes[i].connectedCell.coordinate - centerPos));
                //Debug.Log(dir);
                if (dir != -1 && Array.Exists(nodes[index].outputDirections, d => d == dir) && Array.Exists(nodes[i].inputDirections, d => d == (dir + 3) % 6))
                {
                    yield return (i, dir);
                }
            }
        }
    }

    public System.Collections.Generic.IEnumerable<(int, int)> connectedCellsTo(int index)
    {
        Vector3 centerPos = nodes[index].connectedCell.coordinate;
        List<Vector3> dList = new List<Vector3> { new Vector3(0, -1, 1), new Vector3(1, -1, 0), new Vector3(1, 0, -1), new Vector3(0, 1, -1), new Vector3(-1, 1, 0), new Vector3(-1, 0, 1) };
        for (int i = 0; i < 100; i++)
        {
            if (nodes[i] != null)
            {
                int dir = dList.FindIndex(d => d == (centerPos - nodes[i].connectedCell.coordinate));
                if (dir != -1 && Array.Exists(nodes[index].inputDirections, d => d == (dir + 3) % 6) && Array.Exists(nodes[i].outputDirections, d => d == dir))
                {
                    yield return (i, (dir + 3) % 6);
                }
            }
        }
    }
}
