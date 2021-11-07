using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Split : Node // Todo
{
    public override void Process()
    {
        if(input[0].sequence.length % 2 == 0){
            Output = new Sequence(input[0].sequence[0..input[0].length].ToArray());
        }
        else{
            Output = new Sequence(input[0].sequence.ToArray());
        }
    }
}
