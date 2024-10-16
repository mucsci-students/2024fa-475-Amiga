using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttachmentTextBehavior : MonoBehaviour
{

    [SerializeField] private StaffMenuManager menuManager;
    [SerializeField] private TextMeshProUGUI description;

    void Update ()
    {
        if (!Input.GetMouseButton(0))
        {
            transform.SetAsLastSibling ();
            transform.position = Input.mousePosition;
            description.text = menuManager.GetAttachmentDescriptionForPosition (transform.localPosition);
            transform.localPosition += new Vector3 (170f, -45f, 0f);
        }
        else
        {
            description.text = "";
        }
    }
}
