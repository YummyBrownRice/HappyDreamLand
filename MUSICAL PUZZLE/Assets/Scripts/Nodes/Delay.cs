using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Delay : Node // Todo
{
    public bool isRight;
    public override void Process()
    {
        Sequence.Beat[] tempSequence = new Sequence.Beat[input[0].sequence.Length];
        if(isRight == true)
        {
            for(int i = 0; i < input[0].sequence.Length-1; i++)
            {
                tempSequence[i+1] = input[0].sequence[i];
            }
            tempSequence[0] = input[0].sequence[input[0].sequence.Length-1];
        }
        else
        {
            for(int i = 0; i < input[0].sequence.Length-1; i++)
            {
                tempSequence[i] = input[0].sequence[i+1];
            }
            tempSequence[0] = input[0].sequence[0];
        }
        Output = new Sequence(tempSequence);
    }
}
