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
    public int damage;

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

    void Awake()
    {
        // Add a Collider2D component
        //gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    public void Initialize(int damage, float speed, float size, float life, Vector2 direction)
    {
        this.damage = damage;
        this.speed = speed;
        this.size = size;
        this.life = life;

        //Update size of components
        transform.localScale = new Vector3(size, size, 1);

        GetComponent<CircleCollider2D>().radius = size / 2;

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
        Debug.Log(collision.gameObject.name);
        // Check if the bullet hits the Player and return (ignore player)
        if (collision.gameObject.name.Equals("Player"))
        {
            return;
        }
        // Check if the bullet hits a tilemap and destroy the tile
        else if (collision.gameObject.GetComponent<Tilemap>() != null)
        {
            Vector2 contactPoint = collision.ClosestPoint(transform.position);  // Get the closest point of contact
            handler.DestroyTile(contactPoint, transform.up);  // Destroy the tile at the contact point
            Destroy(gameObject);
        }
        // Check if the bullet hits an enemy and apply damage
        else if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
