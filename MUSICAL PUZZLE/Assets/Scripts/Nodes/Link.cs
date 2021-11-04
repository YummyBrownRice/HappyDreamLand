using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Link : Node
{
    public override void Process()
    {
        Output = new Sequence(input[0].sequence.Concat(input[1].sequence).ToArray());
    }
}
