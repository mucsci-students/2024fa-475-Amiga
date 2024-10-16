using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSizeIncrease : AttackAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        bulletSizeIncrease = 1.0f;
    }

    public override string GetDescription()
    {
        return "This attachment is deprecated!";
    }

    public override void Buff(float ratio)
    {
        bulletSizeIncrease *= ratio;
    }
}
