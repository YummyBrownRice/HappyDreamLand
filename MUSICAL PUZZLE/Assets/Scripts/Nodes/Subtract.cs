using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Subtract : Node // Todo
{
    public override void Process()
    {
        leninput_1 = input[0].sequence.length;
        leninput_2 = input[1].sequence.length;
        
        Output = new Sequence(input[0].sequence.ToArray());
    }
}
