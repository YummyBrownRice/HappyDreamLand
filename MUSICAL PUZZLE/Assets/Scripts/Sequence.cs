using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Sequence
{
    public Beat[] sequence;

    public Sequence(Beat[] sequence_)
    {
        sequence = sequence_;
    }

    public Sequence(int[] sequenceArray)
    {
        List<Beat> sequence_ = new List<Beat>();

        foreach (var beat in sequenceArray)
        {
            sequence_.Add((Beat)beat);
        }

        sequence = sequence_.ToArray();
    }

    [Flags] public enum Beat
    {
        None = 0,
        Kick = 1,
        HiHat = 2
    }
}
