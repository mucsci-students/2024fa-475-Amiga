using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAttachment : Attachment
{
    /// <summary>
    /// The damage increase amount of the attachment.
    /// </summary>
    public int bulletDamageIncrease;

    /// <summary>
    /// The count increase amount of the attachment.
    /// </summary>
    public int bulletCountIncrease;

    /// <summary>
    /// The speed increase amount of the attachment.
    /// </summary>
    public int bulletSpeedIncrease;

    /// <summary>
    /// The size increase amount of the attachment.
    /// </summary>
    public int bulletSizeIncrease;

    /// <summary>
    /// The life increase amount of the attachment.
    /// </summary>
    public int bulletLifeIncrease;

    void Start()
    {
        bulletDamageIncrease = 10;
        bulletCountIncrease = 0;
        bulletSpeedIncrease = 0;
        bulletSizeIncrease = 0;
        bulletLifeIncrease = 0;
    }

    public override void Attach(Staff staff)
    {
        staff.bulletDamage += bulletDamageIncrease;
        staff.bulletCount  += bulletCountIncrease;
        staff.bulletSpeed  += bulletSpeedIncrease;
        staff.bulletSize   += bulletSizeIncrease;
        staff.bulletLife   += bulletLifeIncrease;

        attached = true;
    }

    public override void Detach(Staff staff)
    {
        staff.bulletDamage -= bulletDamageIncrease;
        staff.bulletCount  -= bulletCountIncrease;
        staff.bulletSpeed  -= bulletSpeedIncrease;
        staff.bulletSize   -= bulletSizeIncrease;
        staff.bulletLife   -= bulletLifeIncrease;

        attached = false;
    }
}
