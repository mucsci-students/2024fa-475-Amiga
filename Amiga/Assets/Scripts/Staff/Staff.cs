using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Staff : MonoBehaviour
{

    /// <summary>
    /// Reference to the bullet prefab
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Reference to the player
    /// </summary>
    private Player player;

    /// <summary>
    /// Reference to the tilemap handler
    /// </summary>
    public TilemapHandler handler;

    /// <summary>
    /// Reference to the sprite renderer component
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Reference to the staff sprites
    /// </summary>
    [SerializeField] private List<Sprite> sprites;

    /// <summary>
    /// Reference to the UI canvas
    /// </summary>
    [SerializeField] private Canvas canvas;

    /// <summary> The attachments on the staff. </summary>
    public List<Attachment> attachments;

    /// <summary> The # of attachments on the staff. </summary>
    public int attachmentCount;

    /// <summary> The # of attachments on the staff. </summary>
    public int staffLevel;

    /// <summary>
    /// The maximum number of attachments the staff can currently take
    /// </summary>
    public int maxAttachmentCount;

    /// <summary>
    /// The maximum level the staff can reach
    /// </summary>
    private int maxStaffLevel = 2;

    /// <summary>
    /// The attachments currently in the player's inventory
    /// </summary>
    public List<Attachment> inventory;

    /// <summary>
    /// Reference to hp diplay UI.
    /// </summary>
    public GameObject hpDisplay;

    /// <summary>
    /// Reference to hp diplay UI.
    /// </summary>
    public GameObject manaDisplay;

    /// <summary>
    /// Reference to hp diplay UI.
    /// </summary>
    public GameObject armorDisplay;

    // Attack properties---------------------------------------------

    /// <summary>
    /// The damage of bullet.
    /// </summary>
    public float bulletDamage;

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
    public float armorDefense;

    /// <summary>
    /// The current defense amount of armor.
    /// </summary>
    public float currentArmorDefense;

    /// <summary>
    /// The recovery speed of armor.(per second)
    /// </summary>
    public float armorRecoverySpeed;

    // Special properties:-------------------------------------------

    /// <summary>
    /// The height of jump
    /// </summary>
    public int jumpHeight;

    /// <summary>
    /// The increase in the player's walk speed
    /// </summary>
    public float speedBoost;

    /// <summary>
    /// Whether or not the player is floating
    /// Use integer instead of bool to avoid multiple attaching
    /// </summary>
    public int floating;

    /// <summary>
    /// How much the player will slow enemies down when firing
    /// </summary>
    public float slowmoEffect = 0.5f;

    /// <summary>
    /// The maximum mana
    /// </summary>
    public float maxMana;

    /// <summary>
    /// The current mana
    /// </summary>
    public float currentMana;

    /// <summary>
    /// The recovery speed of mana.(per second)
    /// </summary>
    public float manaRecoverySpeed;

    /// <summary>
    /// The cost of mana for each attack
    /// </summary>
    public int manaCost;

    /// <summary>
    /// The maximum health
    /// </summary>
    public float maxHealth;

    /// <summary>
    /// The current health
    /// </summary>
    public float currentHealth;

    /// <summary>
    /// The recovery speed of health.(per second)
    /// </summary>
    public float healthRecoverySpeed;

    void Start()
    {

        // start with 3 empty slots
        staffLevel = 0;
        maxAttachmentCount = 3;

        attachments = new List<Attachment>();
        for (int i = 0; i < maxAttachmentCount; ++i)
        {
            attachments.Add(null);
        }

        attachmentCount = 0;

        // start with an inventory of 30 empty slots
        inventory = new List<Attachment> ();
        for (int i = 0; i < 30; ++i)
        {
            inventory.Add(null);
        }

        // update sprite & staff position
        spriteRenderer = GetComponent<SpriteRenderer> ();
        spriteRenderer.sprite = sprites[0];
        UpdatePosition (false, false);

        bulletDamage = 10.0f;
        bulletCount = 1;
        bulletSpeed = 7.0f;
        bulletSize = 0.5f;
        bulletLife = 1.0f;

        armorDefense = 50.0f;
        currentArmorDefense = 50.0f;
        armorRecoverySpeed = 5.0f;
        jumpHeight = 10;
        floating = 0;
        slowmoEffect = 1f;
        maxMana = 100.0f;
        currentMana = maxMana;
        manaRecoverySpeed = 20.0f;
        manaCost = 10;

        maxHealth = 100.0f;
        currentHealth = maxHealth;
        healthRecoverySpeed = 5.0f;

        player = transform.parent.gameObject.GetComponent<Player> ();
    }

    void Update()
    {
        Recover();

        hpDisplay.GetComponent<HP>().UpdateHP(currentHealth, maxHealth);
        manaDisplay.GetComponent<MANA>().UpdateMANA(currentMana, maxMana);
        armorDisplay.GetComponent<ARMOR>().UpdateARMOR(currentArmorDefense, armorDefense);

        // bring timeScale back to 1 if not paused
        if (Time.timeScale < 1f && Time.timeScale != 0f)
        {
            Time.timeScale += Time.deltaTime;
        }
    }

    /// <summary>
    /// Reset all stats of staff except for staff level.
    /// </summary>
    public void Reset()
    {
        for (int i = 0; i < attachments.Count; ++i)
        {
            DetachAttachment(i);
        }
        for (int i = 0; i < inventory.Count; ++i)
        {
            DiscardAttachment(i);
        }

        currentArmorDefense = armorDefense;
        currentHealth = maxHealth;
        currentMana = maxMana;
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
        if (attachment != null && index < maxAttachmentCount && attachments[index] == null)
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
        if (index < maxAttachmentCount && attachments[index] != null)
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
    /// Get the next staff slot that an attachment should be put in.
    /// </summary>
    /// <returns> A valid index of a staff slot. </returns>
    public int GetNextAttachmentIndex()
    {
        if (attachmentCount == maxAttachmentCount) return -1;
        else
        {
            for (int i = 0; i < maxAttachmentCount; ++i)
            {
                if (attachments[i] == null) return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Get the next inventory slot that an attachment should be put in.
    /// </summary>
    /// <returns> A valid index of an inventory slot. </returns>
    public int GetNextInventoryIndex()
    {
        for (int i = 0; i < inventory.Count; ++i)
        {
            if (inventory[i] == null) return i;
        }
        return -1;
    }

    /// <summary>
    /// Try to store the given attachment at the given index.
    /// </summary>
    /// <param name="attachment"> the attachment to store </param>
    /// <param name="index"> the index to store to </param>
    /// <returns></returns>
    public bool StoreAttachment (Attachment attachment, int index)
    {
        if (attachment != null && inventory[index] == null)
        {
            inventory[index] = attachment;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Try to discard the attachment at the given index.
    /// </summary>
    /// <param name="index"> the index to discard from </param>
    /// <returns></returns>
    public bool DiscardAttachment (int index)
    {
        if (inventory[index] != null)
        {
            inventory[index] = null;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Lauch bullets toward the mouse position.
    /// </summary>
    public void Launch()
    {
        // Get the mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Adjust the staff position(Not sure why it is below the center of the sprite
        Vector2 staffPosition = (Vector2)transform.position + new Vector2(0.0f, 1.22f);

        // Calculate direction from player to mouse
        Vector2 direction = (mousePosition - staffPosition).normalized;

        if (manaCost <= 0)
        {
            for (int i = 0; i < bulletCount; ++i)
            {
                Shoot(direction);
                currentMana = Mathf.Max(maxMana, currentMana - manaCost);
            }
        }
        else
        {
            for (int i = 0; i < Mathf.Min(bulletCount, currentMana / manaCost); ++i)
            {
                Shoot(direction);
                currentMana -= manaCost;
            }
        }

        SetSlowmo (slowmoEffect);
    }

    /// <summary>
    /// Shoot a bullet.
    /// </summary>
    private void Shoot(Vector2 direction)
    {
        // TODO: avoid bullet overlap

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
    /// </summary>
    public void Recover()
    {
        currentArmorDefense = Mathf.Min(armorDefense, currentArmorDefense + armorRecoverySpeed * Time.deltaTime);
        currentHealth = Mathf.Min(maxHealth, currentHealth + healthRecoverySpeed * Time.deltaTime);
        currentMana = Mathf.Min(maxMana, currentMana + manaRecoverySpeed * Time.deltaTime);
    }

    /// <summary>
    /// Take given amount of damage.
    /// </summary>
    /// <param name="damage"> The amount of damage taken. </param>
    /// <returns> true for alive, false for dead </returns>
    public virtual bool TakeDamage(float damage, bool skipArmor = false)
    {
        if (!skipArmor && currentArmorDefense > 0.0f)
        {
            currentArmorDefense = Mathf.Max(0.0f, currentArmorDefense - damage);
            player.anim.SetTrigger ("Shield Damage"); // play a shielding animation
        }
        else if (currentHealth <= damage)
        {
            return false;
        }
        else
        {
            currentHealth -= damage;
            player.anim.SetTrigger ("Take Damage"); // play a hurting animation
        }
        return true;
    }

    // levels up the staff (add 2 attachment slots) if able
    public void LevelUpStaff ()
    {
        if (staffLevel < maxStaffLevel)
        {
            ++staffLevel;
            maxAttachmentCount += 2;
            attachments.Add (null);
            attachments.Add (null);
            spriteRenderer.sprite = sprites[staffLevel];
            GetComponent<AudioSource> ().Play ();
        }
    }

    public void UpdatePosition (bool isAttacking, bool flipX)
    {
        int flip = flipX ? -1 : 1;
        if (isAttacking)
        {
            transform.localPosition = new Vector3 (flip * 0.45f, 0.35f, 0f);
            transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
        }
        else
        {
            transform.localPosition = new Vector3 (flip * 0.35f, 0f, 0f);
            transform.rotation = Quaternion.Euler (flip * new Vector3 (0f, 0f, -30f));
        }
    }

    // A method to set the game speed, used by the pause menu
    public void SetSlowmo (float speed)
    {
        Time.timeScale = speed;
    }
}
