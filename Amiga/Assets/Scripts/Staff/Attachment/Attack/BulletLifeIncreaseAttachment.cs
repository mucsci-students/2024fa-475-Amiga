using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeIncrease : AttackAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        bulletLifeIncrease = 1.0f;
    }

    public override string GetDescription()
    {
        return "Increase Bullet Life by 1";
    }
}
