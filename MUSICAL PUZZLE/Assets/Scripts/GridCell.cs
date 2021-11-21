using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GridCell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject highlight;
    public Transform indicator;
    public Transform muteIndicator;

    public Color inputColor;
    public Color outputColor;

    public List<Sprite> GridSprites;
    public Sprite nonSprite;
    public int index;
    public Vector3 coordinate;

    public bool locked = false;

    public NodeManager nodeManager;
    private GridManager gridManager;
    private UIManager uiManager;

    public Node connectedNode;

    public bool mouseOn;

    public GameObject draggedOBJ;

    public void Awake()
    {
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && mouseOn)
        {
            //Debug.Log("HIHI");
            if (connectedNode != null)
            {
                connectedNode.muted = !connectedNode.muted;
                nodeManager.UpdateBeat();
                muteIndicator.gameObject.SetActive(!muteIndicator.gameObject.activeInHierarchy);
            }
            
        }
        else if (Input.GetMouseButtonDown(0) && mouseOn && !locked)
        {
            GameObject OBJ = Instantiate(draggedOBJ, transform.position, Quaternion.identity);
            OBJ.GetComponent<DraggedGrid>().type = connectedNode.nodeType;
            OBJ.GetComponent<DraggedGrid>().linkedUI = uiManager.nodeUIs[(int)connectedNode.nodeType];

            nodeManager.RemoveNode(connectedNode.index);
        }
    }

    public void ConnectToNode(int index)
    {
        connectedNode = nodeManager.nodes[index];
        connectedNode.connectedCell = this;

        spriteRenderer.sprite = GridSprites[(int)connectedNode.nodeType];

        transform.Rotate(Vector3.forward * -60 * nodeManager.nodes[index].rotation);

        int i = 0;
        foreach (Transform it in indicator)
        {
            //Debug.Log(it.name);
            int r = connectedNode.rotation;
            int[] inputDir = (int[])connectedNode.inputDirections.Clone();
            int[] outputDir = (int[])connectedNode.outputDirections.Clone();
            SpriteRenderer item = it.GetComponent<SpriteRenderer>();

            for (int a = 0; a < inputDir.Length; a++)
            {
                inputDir[a] = (inputDir[a] - r + 6) % 6;
            }
            for (int a = 0; a < outputDir.Length; a++)
            {
                outputDir[a] = (outputDir[a] - r + 6) % 6;
            }

            if (inputDir.Contains(i))
            {
                //Debug.Log("YO" + i.ToString());
                item.color = inputColor;
            }
            if (outputDir.Contains(i))
            {
                //Debug.Log("HEO" + i.ToString());
                item.color = outputColor;
            }
            i += 1;
        }
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
        highlight.SetActive(true);
        if (connectedNode != null)
        {
            indicator.gameObject.SetActive(true);
        }
        
    }

    private void OnMouseExit()
    {
        mouseOn = false;
        gridManager.selectedCell = null;
        highlight.SetActive(false);
        indicator.gameObject.SetActive(false);
    }
}
