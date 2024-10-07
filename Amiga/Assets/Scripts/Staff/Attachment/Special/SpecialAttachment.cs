using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttachment : Attachment
{
    /// <summary>
    /// The jump height increase amount of the attachment.
    /// </summary>
    public int jumpHeightIncrease = 0;

    /// <summary>
    /// Whether or not is floating
    /// Use integer instead of bool to avoid multiple attaching
    /// </summary>
    public int floating = 0;

    /// <summary>
    /// The maximum mana increase amount of the attachment
    /// </summary>
    public int manaIncrease = 0;

    /// <summary>
    /// The mana recovery speed increase amount of the attachment
    /// </summary>
    public int manaRecoverySpeedIncrease = 0;

    /// <summary>
    /// The mana cost decrease amount of the attachment
    /// </summary>
    public int manaCostDecrease = 0;

    public override void Attach(Staff staff)
    {
        staff.jumpHeight += jumpHeightIncrease;
        staff.floating += floating;
        staff.maxMana += manaIncrease;
        staff.manaRecoverySpeed += manaRecoverySpeedIncrease;
        staff.manaCost -= manaCostDecrease;

        attached = true;
    }

    public override void Detach(Staff staff)
    {
        staff.jumpHeight -= jumpHeightIncrease;
        staff.floating -= floating;
        staff.maxMana -= manaIncrease;
        staff.manaRecoverySpeed -= manaRecoverySpeedIncrease;
        staff.manaCost += manaCostDecrease;

        attached = false;
    }
}
