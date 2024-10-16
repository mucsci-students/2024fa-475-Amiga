using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDefenseIncreaseAttachment : DefenseAttachment
{
    void Start()
    {
        armorDefenseIncrease = 10;
    }

    public override string GetDescription()
    {
        return "Increase Armor by 10";
    }
}
