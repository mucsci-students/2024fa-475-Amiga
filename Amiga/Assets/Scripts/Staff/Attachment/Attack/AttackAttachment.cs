using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAttachment : Attachment
{
    /// <summary>
    /// The damage increase amount of the attachment.
    /// </summary>
    public int bulletDamageIncrease = 0;

    /// <summary>
    /// The count increase amount of the attachment.
    /// </summary>
    public int bulletCountIncrease = 0;

    /// <summary>
    /// The speed increase amount of the attachment.
    /// </summary>
    public float bulletSpeedIncrease = 0.0f;

    /// <summary>
    /// The size increase amount of the attachment.
    /// </summary>
    public float bulletSizeIncrease = 0.0f;

    /// <summary>
    /// The life increase amount of the attachment.
    /// </summary>
    public float bulletLifeIncrease = 0.0f;

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
