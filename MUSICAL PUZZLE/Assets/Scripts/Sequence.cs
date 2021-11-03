using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable]
public class Sequence
{
    public Beat[] sequence;

    public Sequence(Beat[] sequence_)
    {
        sequence = sequence_;
    }
    [Flags] public enum Beat
    {
        None = 0,
        Kick = 1,
        HiHat = 2
    }
}
