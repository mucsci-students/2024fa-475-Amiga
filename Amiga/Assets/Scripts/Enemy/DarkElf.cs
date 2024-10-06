using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DarkElf : GroundEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        // Dark Elf properties:
        // Health: low
        // Speed:  medium
        // Range:  far
        // DPS:    high
        health = 10;
        speed = 1;
        range = 25;
        dps = 20;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }
}
