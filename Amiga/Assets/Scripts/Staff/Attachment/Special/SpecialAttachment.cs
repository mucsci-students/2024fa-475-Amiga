using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttachment : Attachment
{
    /// <summary>
    /// The jump height increase amount of the attachment.
    /// </summary>
    public int jumpHeightIncrease;

    void Start()
    {
        jumpHeightIncrease = 5;
    }

    public override void Attach(Staff staff)
    {
        staff.jumpHeight += jumpHeightIncrease;

        attached = true;
    }

    public override void Detach(Staff staff)
    {
        staff.jumpHeight -= jumpHeightIncrease;

        attached = false;
    }
}
