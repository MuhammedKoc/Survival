using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotClickableObject : MonoBehaviour, IPointerClickHandler
{
    private InventorySwapper inventorySwap;

    private void Start()
    {
        inventorySwap = transform.parent.parent.GetComponent<InventorySwapper>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            inventorySwap.PerformSlotLeftInteraction(this.gameObject);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventorySwap.PerformSlotRightInteraction(this.gameObject);
        }
    }
}
