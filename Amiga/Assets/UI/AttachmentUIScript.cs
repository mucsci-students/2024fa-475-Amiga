using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttachmentUIScript : MonoBehaviour
{

    [SerializeField] private Canvas canvas;

    public void DragHandler (BaseEventData data)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle ((RectTransform) canvas.transform, ((PointerEventData) data).position, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint (position);
    }
}
