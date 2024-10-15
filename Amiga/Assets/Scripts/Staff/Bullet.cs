using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Reference to the tilemap handler
    /// </summary>
    public TilemapHandler handler;

    /// <summary>
    /// The damage of bullet.
    /// </summary>
    public float damage;

    /// <summary>
    /// The speed of bullet.
    /// </summary>
    public float speed;

    /// <summary>
    /// The size of bullet.
    /// </summary>
    public float size;

    /// <summary>
    /// The life of bullet.
    /// </summary>
    public float life;

    public void Initialize(float damage, float speed, float size, float life, Vector2 direction)
    {
        this.damage = damage;
        this.speed = speed;
        this.size = size;
        this.life = life;

        //Update size of components
        transform.localScale = new Vector3(size, size, 1);

        GetComponent<BoxCollider2D>().size *= size;

        transform.right = direction;
    }

    // Update is called once per frame
    void Update()
    {
        // Move bullet by modifying its position directly
        transform.position += speed * Time.deltaTime * transform.right;

        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits the Player and return (ignore player)
        if (collision.gameObject.name.Equals("Player"))
        {
            return;
        }
        // Check if the bullet hits a tilemap and destroy the tile
        // TODO: only destroy tile if the attachment is attached
        else if (collision.gameObject.GetComponent<Tilemap>() != null)
        {
            Vector2 contactPoint = collision.ClosestPoint(transform.position);  // Get the closest point of contact
            
            // Not sure why, but the impactPos need adjustment
            Vector3 adjustedImpactPos = contactPoint + new Vector2(
            transform.right.x > 0 ? 0.05f : -0.05f,  // Adjust based on horizontal direction
            transform.right.y > 0 ? 0.05f : -0.05f);  // Adjust based on vertical direction);

            handler.DestroyTile(adjustedImpactPos, transform.right * 3);  // Destroy the tile at the contact point
            Destroy(gameObject);
        }
        // Check if the bullet hits an enemy and apply damage
        else if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        // Check if the bullet hits a statue and destroy the statue
        // Bullets should always destroy statues, even if the attachment is not attached
        else if (collision.gameObject.GetComponent<StatueBehavior> () != null)
        {
            Vector2 contactPoint = collision.ClosestPoint(transform.position);
            
            Vector3 adjustedImpactPos = contactPoint + new Vector2(
            transform.right.x > 0 ? 0.05f : -0.05f,
            transform.right.y > 0 ? 0.05f : -0.05f);

            collision.gameObject.GetComponent<StatueBehavior> ().DestroyStatue (adjustedImpactPos, transform.right * 3);
            Destroy (gameObject);
        }
    }
}
