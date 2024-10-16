using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorRecoverySpeedIncreaseAttachment : DefenseAttachment
{
    void Start()
    {
        armorRecoverySpeedIncrease = 5;
    }

    public override string GetDescription()
    {
        return "Increase Armor Recovery Speed by 5";
    }
}
