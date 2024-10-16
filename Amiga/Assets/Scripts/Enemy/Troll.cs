using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : GroundEnemy
{

    [SerializeField] private List<AudioClip> attackSounds;

    // Start is called before the first frame update
    void Start()
    {

        // Start invoking the Attack method every 1 second
        InvokeRepeating(nameof(Attack), 1.0f, 1.0f);

        // Troll properties:
        // Health: high
        // Speed:  slow
        // Range:  short
        // DPS:    high
        int level = player.GetComponent<Player>().gameManager.GetComponent<GameManager>().level;
        // Don't grow too fast
        float ration = 1.0f + 0.1f * level;
        health = 50.0f * ration;
        speed = 1.0f * ration;
        range = 2.5f;
        dps = 40.0f * ration;
        direction = 1;
        flipX = true;

        anim.SetFloat ("Speed", 1 + speed / 4f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();

        // move or attack
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
