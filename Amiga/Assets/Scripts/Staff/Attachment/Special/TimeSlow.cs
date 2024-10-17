using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowAttachment : SpecialAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        slowmo = 0.6f;
    }
    public override string GetDescription()
    {
        return "Slow Time";
    }

    public override void Buff(float ratio)
    {
        slowmo = (int)(slowmo * ratio);
    }
}
