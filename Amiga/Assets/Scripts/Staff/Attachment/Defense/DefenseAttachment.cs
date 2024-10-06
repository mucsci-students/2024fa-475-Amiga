using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseAttachment : Attachment
{
    /// <summary>
    /// The defense increase amount of the attachment.
    /// </summary>
    public int armorDefenseIncrease;

    /// <summary>
    /// The recovery speed increase amount of the attachment.
    /// </summary>
    public int armorRecoverySpeedIncrease;

    void Start()
    {
        armorDefenseIncrease = 10;
        armorRecoverySpeedIncrease = 0;
    }

    public override void Attach(Staff staff)
    {
        staff.armorDefense       += armorDefenseIncrease;
        staff.armorRecoverySpeed += armorRecoverySpeedIncrease;

        attached = true;
    }

    public override void Detach(Staff staff)
    {
        staff.armorDefense       -= armorDefenseIncrease;
        staff.armorRecoverySpeed -= armorRecoverySpeedIncrease;

        attached = false;
    }
}
