using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : Node
{

    public Sequence sourceSound;

    public override void Process()
    {
        Output = sourceSound;
    }
}
