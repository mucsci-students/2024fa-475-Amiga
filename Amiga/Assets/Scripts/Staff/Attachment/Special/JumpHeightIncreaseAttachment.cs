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
        return "Increase Jump Height by 5";
    }
}
