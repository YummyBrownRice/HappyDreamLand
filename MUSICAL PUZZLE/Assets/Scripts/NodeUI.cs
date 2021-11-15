using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NodeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public NodeManager.nodeType type;
    private bool mouse_over = false;
    public int count;
    public TMP_Text countText;

    public GameObject draggedOBJ;

    private void Start()
    {
        countText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (mouse_over)
        {
            if (Input.GetMouseButtonDown(0) && count > 0)
            {
                //Debug.Log("HIHI");
                GameObject OBJ = Instantiate(draggedOBJ, transform.position, Quaternion.identity);
                OBJ.GetComponent<DraggedGrid>().type = type;
                OBJ.GetComponent<DraggedGrid>().linkedUI = this;
                count--;
                UpdateCountText();
                //Debug.Log(type);
                //Debug.Log(draggedOBJ.GetComponent<DraggedGrid>().type);
            }
        }
    }

    public void UpdateCountText()
    {
        countText.text = count.ToString();

        if (count <= 0)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
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
