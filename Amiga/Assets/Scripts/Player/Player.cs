using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

        // TODO: sound effects for the attack
    }
}
