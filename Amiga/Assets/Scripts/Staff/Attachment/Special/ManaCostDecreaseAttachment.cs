using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCostDecreaseAttachment : SpecialAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        manaCostDecrease = 10;
    }
    public override string GetDescription()
    {
        return "Decrease Mana Cost by 10";
    }
}
