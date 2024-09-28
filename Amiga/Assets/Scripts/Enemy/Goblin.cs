using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : GroundEnemy
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
        // Speed:  medium
        // Range:  medium
        // DPS:    medium
        health = 100;
        speed = 50;
        range = 10;
        dps = 50;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // move or attack
        Move();
    }
}
