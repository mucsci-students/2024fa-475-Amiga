using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Reference to the bullet prefab
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// The staff used by the player.
    /// All mutable properties are defined in staff.
    /// </summary>
    public Staff staff;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Add Rigidbody2D component
        gameObject.AddComponent<Rigidbody2D>();

        // Add a Collider2D component
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize staff game object with player.
        staff = new GameObject("Staff").GetComponent<Staff>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    /// <summary>
    /// Move the player by key input.
    /// </summary>
    public void Move()
    {
        // Get input from WASD or arrow keys
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveY = Input.GetAxis("Vertical"); // W/S or Up/Down arrows

        // Create a movement vector
        Vector2 movement = new Vector2(moveX, moveY).normalized;

        // Set the velocity of the Rigidbody2D
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    /// <summary>
    /// Attack action of the player.
    /// </summary>
    public void Attack()
    {
        // Get the mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from player to mouse
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        for (int i = 0; i < staff.bulletCount; ++i)
        {
            // TODO: avoid overlap
            shoot(direction);
        }
    }

    /// <summary>
    /// Shoot a bullet.
    /// </summary>
    private void shoot(Vector2 direction)
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Set bullet properties (speed, size, damage)
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Initialize(staff.bulletDamage, staff.bulletSpeed, staff.bulletSize, staff.bulletLife);

        // Set bullet rotation to face the direction of the mouse
        // The bullet sprite should be pointing up
        bullet.transform.up = direction;
    }
}
