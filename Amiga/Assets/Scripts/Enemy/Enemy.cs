using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    /// <summary>
    /// Reference to the player
    /// </summary>
    public GameObject player;

    /// <summary> The health of the enemy. </summary>
    public int health;

    /// <summary> The speed of the enemy. </summary>
    public float speed;

    /// <summary> The attack range of the enemy. </summary>
    public float range;

    /// <summary> The damage per second of the enemy. </summary>
    public int dps;

    /// <summary>
    /// Attack action of the enemy.
    /// </summary>
    public virtual void Attack()
    {
        // TODO: each enemy should have its own attck action
        // Calculate the distance between the two GameObjects
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Check if the distance is less than the attack range
        if (distance < range)
        {
            player.GetComponent<Player>().TakeDamage(dps);
        }
    }

    /// <summary>
    /// Take given amount of damage.
    /// </summary>
    /// <param name="damage"> The amount of damage taken. </param>
    public virtual void TakeDamage(int damage)
    {
        if (health <= damage)
        {
            Die();
        }
        else
        {
            health -= damage;
        }
    }

    /// <summary>
    /// Die action of the enemy.
    /// </summary>
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        // Add Rigidbody2D component
        gameObject.AddComponent<Rigidbody2D>();

        // Add a Collider2D component
        gameObject.AddComponent<BoxCollider2D>();
    }
}
