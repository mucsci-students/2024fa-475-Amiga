using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Goblin : GroundEnemy
{

    [SerializeField] private List<AudioClip> attackSounds;

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
        speed = 2.0f;
        range = 1.5f;
        dps = 10.0f;
        direction = 1;
        flipX = false;

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
        if (distance < range)
        {
            player.GetComponent<Player>().TakeDamage(dps);
            anim.SetTrigger ("Attack");
            attackSrc.clip = attackSounds[Random.Range (0, attackSounds.Count)];
            attackSrc.Play ();
        }
    }
}
