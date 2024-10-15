using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Audio;

public class EnemyBullet : MonoBehaviour
{
    /// <summary>
    /// Reference to the player
    /// </summary>
    public GameObject player;

    /// <summary>
    /// The damage of bullet.
    /// </summary>
    public float damage;

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

    /// <summary>
    /// Whether the bullet is still alive or not.
    /// 
    /// To keep the bullet's impact sound playing, it hides itself
    /// whenever it collides with the player but does not destroy
    /// itself until the audio is finished playing.
    /// </summary>
    public bool dead = false;

    /// <summary>
    /// The audio source component.
    /// </summary>
    public AudioSource src;

    /// <summary>
    /// The sounds that this bullet can make when it hits something.
    /// </summary>
    public List<AudioClip> sounds;

    public void Initialize(float damage, Vector2 direction)
    {
        this.damage = damage;
        this.speed = 5.0f;
        this.size = 1.0f;
        this.life = 3.0f;

        GetComponent<BoxCollider2D>().size *= 2;

        transform.right = direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            // Move bullet by modifying its position directly
            transform.position += speed * Time.deltaTime * transform.right;

            life -= Time.deltaTime;
            if (life <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits the Player
        if (collision.gameObject.name.Equals("Player"))
        {
            player.GetComponent<Player>().TakeDamage(damage);
            src.clip = sounds[Random.Range (0, sounds.Count)];
            src.Play ();
            GetComponent<SpriteRenderer> ().enabled = false;
            GetComponent<Collider2D> ().enabled = false;
            dead = true;
            Destroy(gameObject, src.clip.length);
        }
    }
}
