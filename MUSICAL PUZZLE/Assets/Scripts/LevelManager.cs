using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TextAsset levelFile;

    public Level Load()
    {
        LevelLoaded levelLoaded = JsonUtility.FromJson<LevelLoaded>(levelFile.text);
        //Debug.Log(level.text);
        //Debug.Log(levelLoaded.endGoal);
        //Debug.Log(levelLoaded.map[0].coordinate[1]);

        Level level = new Level();
        level.endGoal = new Sequence(levelLoaded.endGoal);
        level.map = new List<Cell>();

        foreach (var cell in levelLoaded.map)
        {
            Vector3 coord = new Vector3(cell.coordinate[0], cell.coordinate[1], cell.coordinate[2]);
            level.map.Add(new Cell(coord, (NodeManager.nodeType) cell.type, new Sequence(cell.sourceSound)));
        }

        return level;

    }

    [System.Serializable]
    public class LevelLoaded
    {
        public int[] endGoal;
        public LevelCell[] map;
    }

    
    public class Level
    {
        public Sequence endGoal;
        public List<Cell> map;
    }

    [System.Serializable]
    public class LevelCell
    {
        public int[] coordinate;
        public int type;
        public int[] sourceSound;
    }

    public class Cell
    {
        public Vector3 coordinate;
        public NodeManager.nodeType type;
        public Sequence sourceSound;

        public Cell(Vector3 coord, NodeManager.nodeType type_, Sequence sourceSound_)
        {
            coordinate = coord;
            type = type_;
            sourceSound = sourceSound_;
        }
    }
}
