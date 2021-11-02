using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sequence
{
    public Beat[] sequence;

    public enum Beat
    {
        Blank,
        Kick,
        HiHat
    }
}
