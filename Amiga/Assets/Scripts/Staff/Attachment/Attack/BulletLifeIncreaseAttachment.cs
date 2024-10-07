using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeIncrease : AttackAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        bulletDamageIncrease = 0;
        bulletCountIncrease = 0;
        bulletSpeedIncrease = 0.0f;
        bulletSizeIncrease = 0.0f;
        bulletLifeIncrease = 1.0f;
    }
}
