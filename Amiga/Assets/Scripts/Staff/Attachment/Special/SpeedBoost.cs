using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostAttachment : SpecialAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        movementSpeedIncrease = 1;
    }
    public override string GetDescription()
    {
        return "Speed Boost";
    }

    public override void Buff(float ratio)
    {
        movementSpeedIncrease = (int)(movementSpeedIncrease * ratio);
    }
}
