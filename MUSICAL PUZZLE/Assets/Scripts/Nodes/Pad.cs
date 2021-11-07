using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Pad : Node //Todo
{
    public bool isRight;
    public override void Process()
    {
        Sequence.Beat[] tempSequence = new Sequence.Beat[input[0].sequence.Length + 1];
        if(isRight == true)
        {
            for(int i = 0; i < input[0].sequence.Length; i++)
            {
                tempSequence[i] = input[0].sequence[i];
            }
            tempSequence[input[0].sequence.Length] = 0;
        }
        else
        {
            for(int i = 0; i < input[0].sequence.Length; i++)
            {
                tempSequence[i+1] = input[0].sequence[i];
            }
            tempSequence[0] = 0;
        }
        Output = new Sequence(tempSequence);
    }
}
