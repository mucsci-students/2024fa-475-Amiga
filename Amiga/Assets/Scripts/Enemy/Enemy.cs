using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class Enemy : MonoBehaviour
{
    /// <summary>
    /// Reference to the player
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Reference to the random attachment prefab generator
    /// </summary>
    public GameObject attachmentGenerator;

    /// <summary> The health of the enemy. </summary>
    public float health;

    /// <summary> The speed of the enemy. </summary>
    public float speed;

    /// <summary> The attack range of the enemy. </summary>
    public float range;

    /// <summary> The damage per second of the enemy. </summary>
    public float dps;

    /// <summary> The enemy's animator component. </summary>
    public Animator anim;

    /// <summary> The enemy's sprite renderer component. </summary>
    public SpriteRenderer spriteRenderer;

    /// <summary> Whether the enemy's sprite should be flipped or not. </summary>
    public bool flipX;

    /// <summary> The enemy's 1st audio source component (for vocal sounds). </summary>
    public AudioSource vocalSrc;

    /// <summary> The enemy's 2nd audio source component (for attacking sounds). </summary>
    public AudioSource attackSrc;

    /// <summary> The enemy's voice lines. </summary>
    public List<AudioClip> vocalSounds;

    /// <summary>
    /// Take given amount of damage.
    /// </summary>
    /// <param name="damage"> The amount of damage taken. </param>
    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        anim.SetTrigger ("Take Damage");
        vocalSrc.clip = vocalSounds[Random.Range (0, vocalSounds.Count)];
        vocalSrc.Play ();
    }

    /// <summary>
    /// Die action of the enemy.
    /// </summary>
    public virtual void CheckDeath()
    {
        if (health <= 0.0f)
        {
            player.GetComponent<Player>().gameManager.GetComponent<GameManager>().NextKill();

            Attachment attachment = attachmentGenerator.GetComponent<RandomAttachmentPrefab>().GenerateAttachement();
            Instantiate(attachment, transform.position, Quaternion.identity);
            int level = player.GetComponent<Player>().gameManager.GetComponent<GameManager>().level;
            // Don't grow too fast
            float ratio = 1.0f + 0.1f * level;
            attachment.Buff(ratio);

            Destroy(gameObject);

            player.GetComponent<Player>().gameManager.GetComponent<GameManager>().DisplayKillText();
        }
    }

    private void Awake()
    {
        // Add Rigidbody2D component
        gameObject.AddComponent<Rigidbody2D>().freezeRotation = true;

        // Add a Collider2D component
        gameObject.AddComponent<BoxCollider2D>();

        // Get the Animator component
        anim = GetComponent<Animator> ();

        // Get the Sprite Renderer componnent
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }
}
