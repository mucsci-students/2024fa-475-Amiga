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
        gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
    }

    public void Initialize(int damage, int speed, int size, int life, Vector2 direction)
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

    // Called when another collider enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            return;
        }
        else
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // Debug.Log("Trigger Entered by: " + other.gameObject.name);
        // Perform actions when the trigger is entered
    }
}
