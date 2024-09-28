using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manbat : AirEnemy
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Goblin properties:
        // Health: medium
        // Speed:  fast
        // Range:  far
        // DPS:    medium
        health = 100;
        speed = 100;
        range = 50;
        dps = 50;
        direction = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // fly or attack
        Fly();
    }
}
