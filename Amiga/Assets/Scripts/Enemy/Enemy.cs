using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    /// <summary> The health of the enemy. </summary>
    public int health;

    /// <summary> The speed of the enemy. </summary>
    public float speed;

    /// <summary> The attack range of the enemy. </summary>
    public float range;

    /// <summary> The damage per second of the enemy. </summary>
    public float dps;

    /// <summary>
    /// Attack action of the enemy.
    /// </summary>
    public abstract void Attack();

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
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
    }
}
