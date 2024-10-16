using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIncreaseAttachment : AttackAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        bulletDamageIncrease = 10;
    }

    public override string GetDescription()
    {
        return "Increase Bullet Damage by 10";
    }

    public override void Buff(float ratio)
    {
        bulletDamageIncrease = (int)(bulletDamageIncrease * ratio);
    }
}
