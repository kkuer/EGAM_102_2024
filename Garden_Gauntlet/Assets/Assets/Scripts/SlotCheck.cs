using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotCheck : MonoBehaviour
{
    public Color CheckSlotContents()
    {
        return GetComponentInChildren<DraggableItem>().color;
    }
}
