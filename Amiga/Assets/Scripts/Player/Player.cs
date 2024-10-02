using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        gameObject.AddComponent<Rigidbody2D>().gravityScale = 9.8f;

        // Add a Collider2D component
        gameObject.AddComponent<BoxCollider2D>();
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
        Vector2 movement = new Vector2(moveX, moveY).normalized * 5.0f;

        // Set the velocity of the Rigidbody2D
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    /// <summary>
    /// Attack action of the player.
    /// </summary>
    public void Attack()
    {
        // If left button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            staff.Launch();
        }
    }

    
}
