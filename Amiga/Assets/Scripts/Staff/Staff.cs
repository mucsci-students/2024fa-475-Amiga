using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    /// <summary> The attachments on the staff. </summary>
    public List<Attachment> attachments;

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
    public int bulletSpeed;

    /// <summary>
    /// The size of bullet.
    /// </summary>
    public int bulletSize;

    /// <summary>
    /// The life of bullet.
    /// </summary>
    public int bulletLife;

    // Defense properties:-------------------------------------------

    /// <summary>
    /// The defense of armor.
    /// </summary>
    public int armorDefense;

    /// <summary>
    /// The recovery speed of armor.(per second)
    /// </summary>
    public int armorRecoverySpeed;

    // Special properties:-------------------------------------------

    public int jumpHeight;

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
            null
        };

        bulletDamage = 10;
        bulletCount = 1;
        bulletSpeed = 2;
        bulletSize = 1;
        bulletLife = 50;
        armorDefense = 10;
        armorRecoverySpeed = 5;
        jumpHeight = 5;
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
            attachments[index] = attachment;
            attachment.Attach(this);
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
            return true;
        }
        else
        {
            return false;
        }
    }
}
