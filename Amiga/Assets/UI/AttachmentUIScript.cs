using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttachmentUIScript : MonoBehaviour
{

    [SerializeField] private Canvas canvas;
    [SerializeField] StaffMenuManager manager;

    private Vector3 lastPosition;

    public void StartOfDrag ()
    {
        lastPosition = transform.localPosition;
    }

    public void DragHandler (BaseEventData data)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle ((RectTransform) canvas.transform, ((PointerEventData) data).position, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint (position);
    }

    public void UpdateAttachment ()
    {
        transform.localPosition = manager.AttachmentUIChange (gameObject, lastPosition);
    }
}
