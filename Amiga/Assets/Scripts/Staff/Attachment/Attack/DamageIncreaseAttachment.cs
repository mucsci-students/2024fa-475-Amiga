using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIncreaseAttachment : AttackAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        bulletDamageIncrease = 10;
        bulletCountIncrease = 0;
        bulletSpeedIncrease = 0.0f;
        bulletSizeIncrease = 0.0f;
        bulletLifeIncrease = 0.0f;
    }
}
