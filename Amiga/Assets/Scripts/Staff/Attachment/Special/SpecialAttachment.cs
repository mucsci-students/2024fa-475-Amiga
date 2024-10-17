using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAttachment : Attachment
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

    /// <summary>
    /// The amount of extra speed the player gets when running
    /// </summary>
    public int movementSpeedIncrease = 0;

    /// <summary>
    /// Whether or not bullets destroy tiles
    /// Use an int so that the debris flies farther if there are more attachments
    /// </summary>
    public int destruction = 0;

    /// <summary>
    /// How much time slows down whenever the player is firing
    /// </summary>
    public float slowmo = 1;

    public override void Attach(Staff staff)
    {
        staff.jumpHeight += jumpHeightIncrease;
        staff.floating += floating;
        staff.maxMana += manaIncrease;
        staff.manaRecoverySpeed += manaRecoverySpeedIncrease;
        staff.manaCost -= manaCostDecrease;
        staff.speedBoost += movementSpeedIncrease;
        staff.destruction += destruction;
        staff.slowmoEffect *= slowmo;

        attached = true;
    }

    public override void Detach(Staff staff)
    {
        staff.jumpHeight -= jumpHeightIncrease;
        staff.floating -= floating;
        staff.maxMana -= manaIncrease;
        staff.manaRecoverySpeed -= manaRecoverySpeedIncrease;
        staff.manaCost += manaCostDecrease;
        staff.speedBoost -= movementSpeedIncrease;
        staff.destruction -= destruction;
        staff.slowmoEffect /= slowmo;

        attached = false;
    }
}
