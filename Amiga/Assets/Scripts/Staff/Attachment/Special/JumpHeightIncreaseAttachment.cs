using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHeightIncreaseAttachment : SpecialAttachment
{
    void Start()
    {
        jumpHeightIncrease = 5;
    }

    public override string GetDescription()
    {
        return "Jump Boost";
    }

    public override void Buff(float ratio)
    {
        jumpHeightIncrease = (int)(jumpHeightIncrease * ratio);
    }
}
