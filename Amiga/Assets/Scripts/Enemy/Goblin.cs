using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : GroundEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        // Start invoking the Attack method every 1 second
        InvokeRepeating(nameof(Attack), 1.0f, 1.0f);

        // Goblin properties:
        // Health: medium
        // Speed:  medium
        // Range:  medium
        // DPS:    medium
        health = 20;
        speed = 1;
        range = 2;
        dps = 10;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
