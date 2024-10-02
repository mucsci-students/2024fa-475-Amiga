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
        // Add Rigidbody2D component
        gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;

        // Add a Collider2D component
        gameObject.AddComponent<CircleCollider2D>();
    }

    private void Start()
    {
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

        // Use Rigidbody2D for movement
        transform.up = direction;
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: something different
        if (collision.gameObject.name.Equals("Player"))
        {
            return;
        }
        else if (collision.gameObject.GetComponent<Tilemap>() != null)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Get the point where the collision occurred
                handler.DestroyTile(contact.point, transform.up);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Debug.Log("Colliding");
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
