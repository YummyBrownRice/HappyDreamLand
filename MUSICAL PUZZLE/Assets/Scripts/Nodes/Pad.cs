using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Pad : Node //Todo
{
    public override void Process()
    {
        sequence Empty_beat = new Sequence.Beat[0];
        Output = new Sequence(input[0].sequence.Concat(Empty_beat.sequence).ToArray());
    }
}
