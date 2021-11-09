using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggedGrid : MonoBehaviour
{
    public NodeManager.nodeType type;
    public int rotation;

    public GridCell OBJ;

    private GridManager gridManager;
    private NodeManager nodeManager;

    private void Start()
    {
        //Debug.Log(type);
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
        GetComponent<SpriteRenderer>().sprite = OBJ.GridSprites[(int)type];
    }

    private void Update()
    {
        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(a.x, a.y, 0);

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("HIHI");
            if (gridManager.selectedCell != null)
            {
                if (gridManager.selectedCell.connectedNode == null)
                {
                    nodeManager.AddNode(nodeManager.nodeKinds[(int)type], gridManager.selectedCell.coordinate, type, rotation);
                }
            }
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Rotate(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Rotate(-1);
        }
    }

    private void Rotate(int n)
    {
        rotation = (rotation + n) % 6;
        transform.Rotate(Vector3.forward * -60 * n);
    }
}
