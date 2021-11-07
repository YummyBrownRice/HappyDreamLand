using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Pad : Node //Todo
{
    public override void Process()
    {
        Output = new Sequence(input[0].sequence.ToArray());
    }
}
