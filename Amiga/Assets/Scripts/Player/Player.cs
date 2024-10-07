using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The staff used by the player.
    /// All mutable properties are defined in staff.
    /// </summary>
    public Staff staff;

    public bool isGrounded;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Add Rigidbody2D component
        //Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        //rb.gravityScale = 9.8f;
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Add a Collider2D component
        //gameObject.AddComponent<BoxCollider2D>();
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
        float moveY = 0.0f;

        // is floating
        if (staff.floating > 0)
        {
            moveY = Input.GetAxis("Vertical"); // W/S or Up/Down arrows
        }

        // Update physics only when needed instead of every frame.
        if (moveX != 0.0f || moveY != 0.0f)
        {
            // Create a movement vector
            Vector2 movement = new Vector2(moveX, moveY).normalized * 5.0f;

            // Set the velocity of the Rigidbody2D for movement
            GetComponent<Rigidbody2D>().velocity = movement;
        }

        // Check for jumping input using 'Space'
        // TODO: Ensure the player is on the ground
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * staff.jumpHeight, ForceMode2D.Impulse);
        }
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

    /// <summary>
    /// Take given amount of damage.
    /// </summary>
    /// <param name="damage"> The amount of damage taken. </param>
    public virtual void TakeDamage(int damage)
    {
        if (staff.TakeDamage(damage))
        {
            // Damage taken
        }
        else
        {
            Die();
        }
    }

    /// <summary>
    /// Die action of the enemy.
    /// </summary>
    public virtual void Die()
    {
        // Reload the current scene
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        if (collision.gameObject.GetComponent<Attachment>() != null)
        {
            staff.AttachAttachment(collision.gameObject.GetComponent<Attachment>(), staff.GetAttachmentCount());

            // TODO: move this to backpack instead of set to inactive
            collision.gameObject.SetActive(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
