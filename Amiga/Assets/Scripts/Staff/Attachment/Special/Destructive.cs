using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionAttachment : SpecialAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        destruction = 1;
    }
    public override string GetDescription()
    {
        return "Destructive";
    }

    public override void Buff(float ratio)
    {
        destruction = (int)(destruction * ratio);
    }
}
