using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Split : Node // Todo
{
    public int splittedLength;
    public override void Process()
    {   
        splittedLength = input[0].sequence.Length/2;
        if(splittedLength*2 == input[0].sequence.Length){
            Sequence.Beat[] tempSequence = new Sequence.Beat[splittedLength];
            for(int i = 0; i < splittedLength; i++)
            {
                tempSequence[i] = input[0].sequence[i];
            }
            Output = new Sequence(tempSequence);
        }
        if(splittedLength*2 != input[0].sequence.Length){
            Sequence.Beat[] tempSequence = new Sequence.Beat[splittedLength+1];
            for(int i = 0; i < splittedLength+1; i++)
            {
                tempSequence[i] = input[0].sequence[i];
            }
            Output = new Sequence(tempSequence);
        }
    }
}
