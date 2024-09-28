using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : GroundEnemy
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Troll properties:
        // Health: high
        // Speed:  slow
        // Range:  short
        // DPS:    high
        health = 500;
        speed = 25;
        range = 5;
        dps = 100;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // move or attack
        Move();
    }
}
