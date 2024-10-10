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

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float defaultSpeed = 5.0f;
    private int lavaDamage = 20; // the amount of damage damaging tiles such as lave do

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

    private void Start ()
    {
        animator = GetComponent<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
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
        if (staff.floating > 0)
        {
            if (GetComponent<Rigidbody2D>().gravityScale != 0.0f)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            }
        }
        else
        {
            if (GetComponent<Rigidbody2D>().gravityScale == 0.0f)
            {
                GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            }
        }

        // Get input from WASD or arrow keys
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveY = 0.0f;

        // update animation
        animator.SetFloat ("Speed", 1.0f + Mathf.Abs (moveX) * (defaultSpeed + staff.speedBoost) * 0.1f);
        if ((moveX < 0 && !spriteRenderer.flipX) || (moveX > 0 && spriteRenderer.flipX))
            spriteRenderer.flipX = moveX < 0;

        // is floating
        if (staff.floating > 0)
        {
            moveY = Input.GetAxis("Vertical"); // W/S or Up/Down arrows
        }

        // Update physics only when needed instead of every frame.
        if (moveX != 0.0f || moveY != 0.0f)
        {
            // Create a movement vector
            Vector2 movement = new Vector2(moveX, moveY).normalized * (defaultSpeed + staff.speedBoost) * Time.deltaTime;

            // Move the player directly
            transform.Translate(movement);
        }

        // Check for jumping input using 'Space'
        if (staff.floating <= 0 && isGrounded && Input.GetKeyDown(KeyCode.Space))
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
        if (Input.GetMouseButton (0)) // update animator
        {
            animator.SetBool ("Is Attacking", true);
            staff.UpdatePosition (true, spriteRenderer.flipX);
        } 
        else
        {
            animator.SetBool ("Is Attacking", false);
            staff.UpdatePosition (false, spriteRenderer.flipX);
        }
    }

    /// <summary>
    /// Take given amount of damage.
    /// </summary>
    /// <param name="damage"> The amount of damage taken. </param>
    public virtual void TakeDamage(float damage)
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
        animator.SetBool ("Is Jumping", false);

        if (collision.gameObject.GetComponent<Attachment>() != null)
        {
            int slotIndex = staff.GetNextAttachmentIndex ();
            if (slotIndex != -1)
            {
                staff.AttachAttachment(collision.gameObject.GetComponent<Attachment>(), slotIndex);
                collision.gameObject.SetActive(false);
            }
            else
            {
                slotIndex = staff.GetNextInventoryIndex ();
                if (slotIndex != -1)
                {
                    staff.StoreAttachment(collision.gameObject.GetComponent<Attachment>(), slotIndex);
                    collision.gameObject.SetActive(false);
                }
            }


            //staff.AttachAttachment(collision.gameObject.GetComponent<Attachment>(), staff.GetNextAttachmentIndex());

            // TODO: move this to backpack instead of set to inactive
            //collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Tilemap>() != null)
        {
            isGrounded = true;
            
            // take damage if lava
            if (String.Equals (collision.gameObject.name, "Damaging Tilemap"))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * staff.jumpHeight, ForceMode2D.Impulse); // jump
                TakeDamage (lavaDamage);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool ("Is Jumping", true);

        if (collision.gameObject.GetComponent<Tilemap>() != null)
        {
            isGrounded = false;
        }
    }
}
