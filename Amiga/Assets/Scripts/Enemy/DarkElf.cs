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

    private float chargeTime = 1.0f; // how long the elf's weapon takes to charge, in seconds
    private float startTimeOfCharge = -1f;

    [SerializeField] AudioClip chargingSound;
    [SerializeField] List<AudioClip> shootSounds;

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
        int level = player.GetComponent<Player>().gameManager.GetComponent<GameManager>().level;
        // Don't grow too fast
        float ration = 1.0f + 0.1f * level;
        health = 10.0f * ration;
        speed = 2.0f;
        range = 15.0f;
        dps = 20.0f * ration;
        direction = 1;
        flipX = true;

        anim.SetFloat ("Speed", 1 + speed / 4f);
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
        // Calculate the distance between the two GameObjects
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Check if the distance is less than the attack range
        if (distance < range && startTimeOfCharge == -1)
        {
            startTimeOfCharge = Time.time;
            anim.SetBool ("Is Charging", true);
            attackSrc.clip = chargingSound;
            attackSrc.Play ();
        }
        else if (startTimeOfCharge != -1 && startTimeOfCharge + chargeTime < Time.time)
        {
            // Calculate direction from enemy to player
            Vector2 direction = (Vector2)(player.transform.position - transform.position).normalized;

            // Instantiate the bullet
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            enemyBullet.GetComponent<EnemyBullet>().player = player;

            // Set bullet properties
            enemyBullet.GetComponent<EnemyBullet>().Initialize(dps, direction);

            startTimeOfCharge = -1f;
            anim.SetBool ("Is Charging", false);
            attackSrc.clip = shootSounds[Random.Range (0, shootSounds.Count)];
            attackSrc.Play ();
        }
    }
}
