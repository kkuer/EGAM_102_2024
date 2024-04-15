using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerPanelSlot : MonoBehaviour, IDropHandler
{
    // (script referenced from youtube tutorial: https://www.youtube.com/watch?v=kWRyZ3hb1Vc)

    public bool hasEntered;

    public FountainMiniGame fountainMiniGame;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
            hasEntered = true;

            if (draggableItem != null && draggableItem.tag == "frog")
            {
                fountainMiniGame.frogsLeft--;
                fountainMiniGame.frogsLeftText.text = fountainMiniGame.frogsLeft.ToString();
                Destroy(draggableItem.gameObject);
            }
            //else if (draggableItem != null && draggableItem.tag == "dangerfrog")
            //{

            //}
        }
    }
}
