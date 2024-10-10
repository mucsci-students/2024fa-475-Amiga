using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttachmentUIScript : MonoBehaviour
{

    [SerializeField] private Canvas canvas;
    [SerializeField] StaffMenuManager manager;
    [SerializeField] private List<Sprite> sprites;
    public Attachment attachment;

    private Vector3 lastPosition;

    private void Start ()
    {
        if (attachment != null) GetComponent<Image> ().sprite = sprites[attachment.spriteNumber];
    }

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

    public void GoToPosition (Vector3 pos)
    {
        transform.localPosition = pos;
    }
}
