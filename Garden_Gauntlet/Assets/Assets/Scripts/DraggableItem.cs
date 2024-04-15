using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // (script referenced from youtube tutorial: https://www.youtube.com/watch?v=kWRyZ3hb1Vc)

    public CanvasGroup canvasGroup;
    public Image image;

    public Color color;

    [HideInInspector] public Transform parentAfterDrag;

    public Transform moveHandles;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");

        // save original parent of item
        parentAfterDrag = transform.parent;
        
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        
        // set new parent of item
        transform.SetParent(parentAfterDrag);

        canvasGroup.alpha = 1;
        image.raycastTarget = true;
    }
}
