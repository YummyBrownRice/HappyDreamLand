using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public List<Sprite> GridSprites;
    public Sprite nonSprite;
    public int index;
    public Vector3 coordinate;

    public NodeManager nodeManager;

    public Node connectedNode;

    public void Awake()
    {
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ConnectToNode(int index)
    {
        connectedNode = nodeManager.nodes[index];
        connectedNode.connectedCell = this;

        spriteRenderer.sprite = GridSprites[(int)connectedNode.nodeType];

        transform.Rotate(Vector3.forward * -60 * nodeManager.nodes[index].rotation);
    }
}
