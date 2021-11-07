using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Delay : Node // Todo
{
    public override void Process()
    {
        Output = new Sequence(input[0].sequence.ToArray());
    }
}
