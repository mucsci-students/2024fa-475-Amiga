using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    /// <summary>
    /// Reference to the player
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Reference to the random attachment prefab generator
    /// </summary>
    public GameObject attachmentGenerator;

    /// <summary> The health of the enemy. </summary>
    public float health;

    /// <summary> The speed of the enemy. </summary>
    public float speed;

    /// <summary> The attack range of the enemy. </summary>
    public float range;

    /// <summary> The damage per second of the enemy. </summary>
    public float dps;

    /// <summary>
    /// Take given amount of damage.
    /// </summary>
    /// <param name="damage"> The amount of damage taken. </param>
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    /// <summary>
    /// Die action of the enemy.
    /// </summary>
    public virtual void CheckDeath()
    {
        if (health <= 0.0f)
        {
            Attachment attachment = attachmentGenerator.GetComponent<RandomAttachmentPrefab>().GenerateAttachement();
            Instantiate(attachment, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        // Add Rigidbody2D component
        gameObject.AddComponent<Rigidbody2D>().freezeRotation = true;

        // Add a Collider2D component
        gameObject.AddComponent<BoxCollider2D>();
    }
}
