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
        health = 20.0f;
        speed = 1.0f;
        range = 10.0f;
        dps = 10.0f;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();

        Move();
    }

    /// <summary>
    /// Attack action of the enemy.
    /// </summary>
    public virtual void Attack()
    {
        // TODO: Attack animation goes here

        // Calculate the distance between the two GameObjects
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Check if the distance is less than the attack range
        if (distance < range)
        {
            player.GetComponent<Player>().TakeDamage(dps);
        }
    }
}
