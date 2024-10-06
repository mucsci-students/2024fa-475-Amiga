using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : GroundEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        // Troll properties:
        // Health: high
        // Speed:  slow
        // Range:  short
        // DPS:    high
        health = 50;
        speed = 0.5f;
        range = 5;
        dps = 40;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // move or attack
        Move();
        Attack();
    }
}
