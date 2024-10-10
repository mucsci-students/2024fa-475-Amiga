using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DarkElf : GroundEnemy
{
    /// <summary>
    /// Reference to the enemy bullet prefab
    /// </summary>
    public GameObject enemyBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Start invoking the Attack method every 1 second
        InvokeRepeating(nameof(Attack), 1.0f, 1.0f);

        // Dark Elf properties:
        // Health: low
        // Speed:  medium
        // Range:  far
        // DPS:    high
        health = 10.0f;
        speed = 1.0f;
        range = 25.0f;
        dps = 20.0f;
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
            // Calculate direction from enemy to player
            Vector2 direction = (Vector2)(player.transform.position - transform.position).normalized;

            // Instantiate the bullet
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            enemyBullet.GetComponent<EnemyBullet>().player = player;

            // Set bullet properties
            enemyBullet.GetComponent<EnemyBullet>().Initialize(dps, direction);
        }
    }
}
