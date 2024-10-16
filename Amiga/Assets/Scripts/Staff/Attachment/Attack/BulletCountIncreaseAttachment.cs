using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCountIncrease : AttackAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        bulletCountIncrease = 1;
    }

    public override string GetDescription()
    {
        return "Increase Bullet Count by 1";
    }
}
