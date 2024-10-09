using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyBullet : MonoBehaviour
{
    /// <summary>
    /// Reference to the player
    /// </summary>
    public GameObject player;

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

    public void Initialize(int damage, Vector2 direction)
    {
        this.damage = damage;
        this.speed = 5.0f;
        this.size = 1.0f;
        this.life = 3.0f;

        GetComponent<BoxCollider2D>().size *= 2;

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
        // Check if the bullet hits the Player
        if (collision.gameObject.name.Equals("Player"))
        {
            player.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
