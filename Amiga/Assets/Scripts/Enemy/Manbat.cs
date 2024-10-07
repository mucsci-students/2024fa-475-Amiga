using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manbat : AirEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;

        // Goblin properties:
        // Health: medium
        // Speed:  fast
        // Range:  far
        // DPS:    medium
        health = 20;
        speed = 2;
        range = 25;
        dps = 10;
        direction = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
        Attack();
    }
}
