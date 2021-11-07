using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject GridCell;
    public int n;
    public float a;

    public Vector2 Origin;

    public List<Vector3> indexToCoordinate;
    public List<GridCell> indexToGridcell;

    // Start is called before the first frame update
    void Awake()
    {
        int index = 1;
        Vector3 InitCoord = new Vector3(0, 0, 0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Vector3 coordinate = InitCoord + j * (new Vector3(0, -1, 1));
                GameObject go = Instantiate(GridCell, Origin + j * (new Vector2(0, a)), Quaternion.identity, transform);
                GridCell goGridcell = go.GetComponent<GridCell>();
                goGridcell.index = index;
                indexToCoordinate.Add(coordinate);
                indexToGridcell.Add(goGridcell);
                goGridcell.coordinate = coordinate;
            }
            Origin = Origin + new Vector2(a * Mathf.Sqrt(3) / 2, Mathf.Pow(-1, i + 1) * (-a / 2));
            if (i % 2 == 0)
            {
                InitCoord = InitCoord + new Vector3(1, 0, -1);
            }
            else
            {
                InitCoord = InitCoord + new Vector3(1, -1, 0);
            }
        }
    }

}
