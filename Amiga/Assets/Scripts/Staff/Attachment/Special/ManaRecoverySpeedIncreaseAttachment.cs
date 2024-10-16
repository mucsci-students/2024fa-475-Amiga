using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRecoverySpeedIncreaseAttachment : SpecialAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        manaRecoverySpeedIncrease = 10;
    }
    public override string GetDescription()
    {
        return "Increase Mana Recovery Speed by 10";
    }

    public override void Buff(float ratio)
    {
        manaRecoverySpeedIncrease = (int)(manaRecoverySpeedIncrease * ratio);
    }
}
