using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public NodeManager.nodeType type;
    private bool mouse_over = false;

    public GameObject draggedOBJ;
    void Update()
    {
        if (mouse_over)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("HIHI");
                GameObject OBJ = Instantiate(draggedOBJ, transform.position, Quaternion.identity);
                OBJ.GetComponent<DraggedGrid>().type = type;
                //Debug.Log(type);
                //Debug.Log(draggedOBJ.GetComponent<DraggedGrid>().type);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        //Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        //Debug.Log("Mouse exit");
    }

    
}