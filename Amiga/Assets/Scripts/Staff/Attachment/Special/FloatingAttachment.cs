using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAttachment : SpecialAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        floating = 1;
    }

    public override string GetDescription()
    {
        return "Enable Floating";
    }

    public override void Buff(float ratio)
    {
    }
}
