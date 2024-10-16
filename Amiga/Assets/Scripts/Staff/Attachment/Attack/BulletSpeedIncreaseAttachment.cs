using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeedIncreaseAttachment : AttackAttachment
{
    // Start is called before the first frame update
    void Start()
    {
        bulletSpeedIncrease = 7.0f;
    }

  public override string GetDescription()
    {
        return "Increase Bullet Speed by 7";
    }
}
