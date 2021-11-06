using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject GridCell;
    public int n;
    public float a;

    public Vector2 Origin;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Instantiate(GridCell, Origin + j * (new Vector2(0, a)), Quaternion.identity, transform);
            }
            Origin = Origin + new Vector2(a * Mathf.Sqrt(3) / 2, Mathf.Pow(-1, i+1) * (- a / 2));
        }
    }

}
