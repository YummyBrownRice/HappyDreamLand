using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private LevelManager levelManager;
    private NodeManager nodeManager;
    public List<LevelManager.Limitation> limits;

    public Transform content;
    public float spacing;
    public float margin;

    public GameObject tileOBJ;
    public GridCell gridCell;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Nodes").GetComponent<LevelManager>();
        nodeManager = GameObject.Find("Nodes").GetComponent<NodeManager>();
        LevelManager.Level level = levelManager.Load();
        limits = level.limitations;
        int n = limits.Count;
        //Debug.Log(n);
        RectTransform r = content.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(r.rect.width, 2 * margin + (n - 1) * (spacing));
        //Debug.Log(r.rect.y);
        Vector2 Pos = new Vector2(0, -margin);
        foreach (var limit in limits)
        {
            
            GameObject go = Instantiate(tileOBJ, new Vector2(0, 0), Quaternion.identity, content);
            go.transform.localPosition = Pos;
            Pos = Pos - new Vector2(0, spacing);
            go.GetComponent<Image>().sprite = gridCell.GridSprites[(int)limit.type];
            //go.transform.name = Pos.y;
        }
    }

}
