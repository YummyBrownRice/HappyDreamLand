using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Sprite GridSprite;

    public List<Sprite> GridSprites;
    public Sprite nonSprite;
    public int index;
    public Vector3 coordinate;

    public NodeManager nodeManager;

    public Node connectedNode;

    public void Awake()
    {
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
    }

    public void ConnectToNode(int index)
    {
        connectedNode = nodeManager.nodes[index];
        connectedNode.connectedCell = this;

        GridSprite = GridSprites[(int)connectedNode.nodeType];
    }
}
