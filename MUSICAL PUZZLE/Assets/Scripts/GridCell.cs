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
    private GridManager gridManager;

    public Node connectedNode;

    public bool mouseOn;

    public void Awake()
    {
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
    }

    private void Update()
    {

    }

    public void ConnectToNode(int index)
    {
        connectedNode = nodeManager.nodes[index];
        connectedNode.connectedCell = this;

        spriteRenderer.sprite = GridSprites[(int)connectedNode.nodeType];

        transform.Rotate(Vector3.forward * -60 * nodeManager.nodes[index].rotation);
    }

    public void EmptyCell()
    {
        connectedNode = null;

        spriteRenderer.sprite = null;

        transform.rotation = Quaternion.identity;
    }

    private void OnMouseEnter()
    {
        mouseOn = true;
        gridManager.selectedCell = this;

    }

    private void OnMouseExit()
    {
        mouseOn = false;
        gridManager.selectedCell = null;
    }
}
