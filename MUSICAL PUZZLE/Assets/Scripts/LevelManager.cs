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
        level.limitations = new List<Limitation>();

        foreach (var cell in levelLoaded.map)
        {
            Vector3 coord = new Vector3(cell.coordinate[0], cell.coordinate[1], cell.coordinate[2]);
            level.map.Add(new Cell(coord, (NodeManager.nodeType) cell.type, cell.rotation, new Sequence(cell.sourceSound)));
        }

        foreach (var limitation in levelLoaded.limitations)
        {
            level.limitations.Add(new Limitation((NodeManager.nodeType)limitation.type, limitation.count));
        }

        return level;

    }

    [System.Serializable]
    public class LevelLoaded
    {
        public int[] endGoal;
        public LevelCell[] map;
        public LimitationLoaded[] limitations;
    }

    
    public class Level
    {
        public Sequence endGoal;
        public List<Cell> map;
        public List<Limitation> limitations;
    }

    [System.Serializable]
    public class LevelCell
    {
        public int[] coordinate;
        public int type;
        public int rotation;
        public int[] sourceSound;
    }

    [System.Serializable]
    public class LimitationLoaded
    {
        public int type;
        public int count;
    }
    
    public class Limitation
    {
        public NodeManager.nodeType type;
        public int count;

        public Limitation(NodeManager.nodeType type_, int count_)
        {
            type = type_;
            count = count_;
        }

    }

    public class Cell
    {
        public Vector3 coordinate;
        public NodeManager.nodeType type;
        public int rotation;
        public Sequence sourceSound;

        public Cell(Vector3 coord, NodeManager.nodeType type_, int rotation_, Sequence sourceSound_)
        {
            coordinate = coord;
            type = type_;
            rotation = rotation_;
            sourceSound = sourceSound_;
        }
    }
}
