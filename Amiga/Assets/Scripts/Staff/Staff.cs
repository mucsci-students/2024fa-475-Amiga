using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Staff : MonoBehaviour
{
    /// <summary>
    /// Reference to the bullet prefab
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Reference to the tilemap handler
    /// </summary>
    public TilemapHandler handler;

    /// <summary> The attachments on the staff. </summary>
    public List<Attachment> attachments;

    /// <summary> The # of attachments on the staff. </summary>
    public int attachmentCount;

    /// <summary>
    /// Reference to hp diplay UI.
    /// </summary>
    public Text hpDisplay;

    /// <summary>
    /// Reference to hp diplay UI.
    /// </summary>
    public Text manaDisplay;

    /// <summary>
    /// Reference to hp diplay UI.
    /// </summary>
    public Text armorDisplay;

    // Attack properties---------------------------------------------

    /// <summary>
    /// The damage of bullet.
    /// </summary>
    public int bulletDamage;

    /// <summary>
    /// The count of bullet.
    /// </summary>
    public int bulletCount;

    /// <summary>
    /// The speed of bullet.
    /// </summary>
    public float bulletSpeed;

    /// <summary>
    /// The size of bullet.
    /// </summary>
    public float bulletSize;

    /// <summary>
    /// The life of bullet.
    /// </summary>
    public float bulletLife;

    // Defense properties:-------------------------------------------

    /// <summary>
    /// The maximum defense amount of armor.
    /// </summary>
    public int armorDefense;

    /// <summary>
    /// The current defense amount of armor.
    /// </summary>
    public int currentArmorDefense;

    /// <summary>
    /// The recovery speed of armor.(per second)
    /// </summary>
    public int armorRecoverySpeed;

    // Special properties:-------------------------------------------

    /// <summary>
    /// The height of jump
    /// </summary>
    public int jumpHeight;

    /// <summary>
    /// The maximum health
    /// </summary>
    public int maxHealth;

    /// <summary>
    /// The current health
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// The recovery speed of health.(per second)
    /// </summary>
    public int healthRecoverySpeed;

    /// <summary>
    /// The maximum mana
    /// </summary>
    public int maxMana;

    /// <summary>
    /// The current mana
    /// </summary>
    public int currentMana;

    /// <summary>
    /// The recovery speed of mana.(per second)
    /// </summary>
    public int manaRecoverySpeed;

    /// <summary>
    /// The cost of mana for each attack
    /// </summary>
    public int manaCost;

    private void Awake()
    {
        // Parent(Player) is already DontDestroyOnLoad
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // start with 3 empty slots
        attachments = new List<Attachment>
        {
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        };

        attachmentCount = 0;

        bulletDamage = 10;
        bulletCount = 1;
        bulletSpeed = 7.0f;
        bulletSize = 0.5f;
        bulletLife = 3.0f;

        armorDefense = 50;
        currentArmorDefense = 50;
        armorRecoverySpeed = 5;

        jumpHeight = 5;
        maxHealth = 100;
        currentHealth = maxHealth;
        healthRecoverySpeed = 5;
        maxMana = 100;
        currentMana = maxMana;
        manaRecoverySpeed = 20;
        manaCost = 10;

        // Start invoking the Recover method every 1 second
        InvokeRepeating(nameof(Recover), 1.0f, 1.0f);
    }

    void Update()
    {
        // Get the mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from staff to mouse
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        transform.up = direction;

        hpDisplay.text = "hp: " + currentHealth.ToString();
        manaDisplay.text = "mana: " + currentMana.ToString();
        armorDisplay.text = "armor: " + currentArmorDefense.ToString();
    }

    /// <summary>
    /// Try to attach the given attachment at the given index.
    /// </summary>
    /// <param name="attachment"> the attachment to attach </param>
    /// <param name="index"> the index to attach to </param>
    /// <returns></returns>
    public bool AttachAttachment(Attachment attachment, int index)
    {
        // Empty slot, avilable to attach
        if (index < attachments.Count && attachments[index] == null)
        {
            attachment.Attach(this);
            attachments[index] = attachment;
            ++attachmentCount;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Try to detach the attachment at the given index.
    /// </summary>
    /// <param name="index"> the index to detach from </param>
    /// <returns></returns>
    public bool DetachAttachment(int index)
    {
        if (index < attachments.Count && attachments[index] != null)
        {
            attachments[index].Detach(this);
            attachments[index] = null;
            --attachmentCount;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Get the # of attachments attached to the staff.
    /// </summary>
    /// <returns> # of attachments attached to the staff. </returns>
    public int GetAttachmentCount()
    {
        return attachmentCount;
    }

    /// <summary>
    /// Lauch bullets toward the mouse position.
    /// </summary>
    public void Launch()
    {
        // Get the mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from player to mouse
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        for (int i = 0; i < Mathf.Min(bulletCount, currentMana / manaCost); ++i)
        {
            // TODO: avoid overlap
            Shoot(direction);
            currentMana -= manaCost;
        }
    }

    /// <summary>
    /// Shoot a bullet.
    /// </summary>
    private void Shoot(Vector2 direction)
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().handler = handler;

        // Set bullet properties (speed, size, damage)
        bullet.GetComponent<Bullet>().Initialize(bulletDamage,
                                                 bulletSpeed,
                                                 bulletSize,
                                                 bulletLife,
                                                 direction);
    }

    /// <summary>
    /// Recover all properties.
    /// Should be called every second.
    /// </summary>
    public void Recover()
    {
        currentArmorDefense = Mathf.Min(armorDefense, currentArmorDefense + armorRecoverySpeed);
        currentHealth = Mathf.Min(maxHealth, currentHealth + healthRecoverySpeed);
        currentMana = Mathf.Min(maxMana, currentMana + manaRecoverySpeed);
    }

    /// <summary>
    /// Take given amount of damage.
    /// </summary>
    /// <param name="damage"> The amount of damage taken. </param>
    /// <returns> true for alive, false for dead </returns>
    public virtual bool TakeDamage(int damage)
    {
        if (currentArmorDefense > 0)
        {
            currentArmorDefense = Mathf.Max(0, currentArmorDefense - damage);
        }
        else if (currentHealth <= damage)
        {
            return false;
        }
        else
        {
            currentHealth -= damage;
        }
        return true;
    }
}
