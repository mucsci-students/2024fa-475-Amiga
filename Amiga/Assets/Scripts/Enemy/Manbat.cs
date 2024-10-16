using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manbat : AirEnemy
{
    /// <summary>
    /// Reference to the enemy bullet prefab
    /// </summary>
    public GameObject enemyBulletPrefab;

    [SerializeField] private List<AudioClip> attackSounds;

    // Start is called before the first frame update
    void Start()
    {

        // Start invoking the Attack method every 1 second
        InvokeRepeating(nameof(Attack), 1.0f, 1.0f);

        GetComponent<Rigidbody2D>().gravityScale = 0.0f;

        // Goblin properties:
        // Health: medium
        // Speed:  fast
        // Range:  far
        // DPS:    medium
        int level = player.GetComponent<Player>().gameManager.GetComponent<GameManager>().level;
        // Don't grow too fast
        float ratio = 1.0f + 0.1f * level;
        health = 20.0f * ratio;
        speed = 4.0f;
        range = 10.0f;
        dps = 10.0f * ratio;
        direction = new Vector2(1, 0);
        flipX = false;

        anim.SetFloat ("Speed", 1 + speed / 4f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();

        Fly();
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

            attackSrc.clip = attackSounds[Random.Range (0, attackSounds.Count)];
            attackSrc.Play ();
        }
    }
}
