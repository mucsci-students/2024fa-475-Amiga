using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// The damage of bullet.
    /// </summary>
    public int damage;

    /// <summary>
    /// The speed of bullet.
    /// </summary>
    public int speed;

    /// <summary>
    /// The size of bullet.
    /// </summary>
    public int size;

    /// <summary>
    /// The life of bullet.
    /// </summary>
    public float life;

    void Awake()
    {
        // Add Rigidbody2D component
        gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;

        // Add a Collider2D component
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
    }

    public void Initialize(int damage, int speed, int size, int life)
    {
        this.damage = damage;
        this.speed = speed;
        this.size = size;
        this.life = life;

        //Update size of components
        transform.localScale = new Vector3(size, size, 1);

        GetComponent<CircleCollider2D>().radius = size / 2;

        // Use Rigidbody2D for movement
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
}
