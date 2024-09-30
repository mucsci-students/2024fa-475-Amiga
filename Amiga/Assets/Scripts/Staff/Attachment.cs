using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attachment : MonoBehaviour
{
    /// <summary>
    /// Whether this attachment is attached.
    /// </summary>
    public bool attached = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Attach this attachment to a staff.
    /// </summary>
    /// <param name="staff"> the staff to attach to </param>
    public abstract void Attach(Staff staff);

    /// <summary>
    /// Detach this attachment from a staff.
    /// </summary>
    /// <param name="staff"> the staff to detach from </param>
    public abstract void Detach(Staff staff);
}
