using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Reference to the game manager.
    /// </summary>
    public GameObject gameManager;

    /// <summary>
    /// The staff used by the player.
    /// All mutable properties are defined in staff.
    /// </summary>
    public Staff staff;

    public int isGrounded;

    public bool isDead;

    public Animator anim;
    private AudioSource src;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private float defaultSpeed = 5.0f;
    private int lavaDamage = 20; // the amount of damage that damaging tiles such as lava do

    public AudioClip shootSound; // the sound of the bullet being shot
    public AudioClip attachSound; // the sound to play when the staff picks up an attachment & attaches it
    public AudioClip pickupSound; // the sound to play when the staff picks up an attachment & puts it in the inventory
    public AudioClip sizzleSound; // the sound to play when the player touches lava

    private void Start ()
    {
        anim = GetComponent<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        rb = GetComponent<Rigidbody2D> ();
        src = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (isDead)
            {
                Die();
            }
            Move();
            Attack();
        }
    }

    /// <summary>
    /// Move the player by key input.
    /// </summary>
    public void Move()
    {
        if (staff.floating > 0)
        {
            if (rb.gravityScale != 0.0f)
            {
                rb.gravityScale = 0.0f;
            }
        }
        else
        {
            if (rb.gravityScale == 0.0f)
            {
                rb.gravityScale = 2.0f;
            }
        }

        // Get input from WASD or arrow keys
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveY = 0.0f;

        // update animation
        anim.SetFloat ("Speed", 1.0f + Mathf.Abs (moveX) * (defaultSpeed + staff.speedBoost) * 0.1f);
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
            Vector2 movement = Time.timeScale == 0 ? new Vector2 (0f, 0f) : new Vector2(moveX, moveY).normalized * (defaultSpeed + staff.speedBoost) * Time.deltaTime / Time.timeScale;

            // Move the player directly
            transform.Translate(movement);
        }

        // Check for jumping input using 'Space', 'W' or the Up Arrow
        if (staff.floating <= 0 && isGrounded > 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) 
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
        if (Input.GetMouseButtonDown(0) && (Input.mousePosition.x < 1335 || Input.mousePosition.x > 1435 || Input.mousePosition.y < 1025 | Input.mousePosition.y > 1130))
        {
            if (staff.Launch())
            {
                src.PlayOneShot (shootSound); // play spell casting sound 
            }
        }

        // update animator
        if (Input.GetMouseButton (0))
        {
            anim.SetBool ("Is Attacking", true);
            staff.UpdatePosition (true, spriteRenderer.flipX);
        } 
        else
        {
            anim.SetBool ("Is Attacking", false);
            staff.UpdatePosition (false, spriteRenderer.flipX);
        }
    }

    /// <summary>
    /// Take given amount of damage.
    /// </summary>
    /// <param name="damage"> The amount of damage taken. </param>
    public virtual void TakeDamage(float damage, bool skipArmor = false)
    {
        if (staff.TakeDamage(damage, skipArmor))
        {
            gameManager.GetComponent<GameManager>().DisplayHurtText();
        }
        else
        {
            isDead = true;
        }
    }

    /// <summary>
    /// Die action of the enemy.
    /// </summary>
    public virtual void Die()
    {
        // Move to the original position
        transform.localPosition = new Vector3(-166.88f, 60.59f, 0);

        staff.Reset();

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        gameManager.GetComponent<GameManager>().DisplayDeathText();

        isDead = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool ("Is Jumping", false);

        if (collision.gameObject.GetComponent<Attachment>() != null)
        {
            int slotIndex = staff.GetNextAttachmentIndex ();
            if (slotIndex != -1)
            {
                staff.AttachAttachment(collision.gameObject.GetComponent<Attachment>(), slotIndex);
                src.PlayOneShot (attachSound);
                collision.gameObject.SetActive(false);

                gameManager.GetComponent<GameManager>().DisplayCollectAttachmentText();
            }
            else
            {
                slotIndex = staff.GetNextInventoryIndex ();
                if (slotIndex != -1)
                {
                    staff.StoreAttachment(collision.gameObject.GetComponent<Attachment>(), slotIndex);
                    src.PlayOneShot (pickupSound);
                    collision.gameObject.SetActive(false);

                    gameManager.GetComponent<GameManager>().DisplayCollectAttachmentText();
                }
            }
        }
        else if (collision.gameObject.GetComponent<Tilemap>() != null)
        {
            ++isGrounded;
            
            // take damage if lava
            if (String.Equals (collision.gameObject.name, "Damaging Tilemap"))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * staff.jumpHeight, ForceMode2D.Impulse); // jump
                TakeDamage (lavaDamage, true);
                src.PlayOneShot (sizzleSound);

                gameManager.GetComponent<GameManager>().DisplayLavaText();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Tilemap>() != null)
        {
            --isGrounded;
        }
        if (isGrounded == 0)
        {
            anim.SetBool("Is Jumping", true);
        }
    }
}
