using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaIncreaseAttachment : SpecialAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        manaIncrease = 10;
    }

    public override string GetDescription()
    {
        return "+ Mana Capacity";
    }

    public override void Buff(float ratio)
    {
        manaIncrease = (int)(manaIncrease * ratio);
    }
}
