using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggedGrid : MonoBehaviour
{
    public NodeManager.nodeType type;
    public int rotation;

    private GridManager gridManager;
    private NodeManager nodeManager;

    private void Start()
    {
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
    }

    private void Update()
    {
        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(a.x, a.y, 0);

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("HIHI");

            nodeManager.AddNode(nodeManager.nodeKinds[(int)type], gridManager.selectedCell.coordinate, type, rotation);
            Destroy(gameObject);
        }
    }
}
