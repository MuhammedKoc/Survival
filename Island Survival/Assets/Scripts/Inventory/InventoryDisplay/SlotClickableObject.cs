using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotClickableObject : MonoBehaviour
{
    private InventorySwapper inventorySwap;
    private InventoryDescription inventoryDescription;

    private void Start()
    {
        inventorySwap = transform.parent.parent.GetComponent<InventorySwapper>();
        inventoryDescription = transform.parent.parent.GetComponent<InventoryDescription>();
    }
    //
    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     if(eventData.button == PointerEventData.InputButton.Left)
    //     {
    //         inventorySwap.SlotLeftClick(this.gameObject);
    //     }
    //     else if (eventData.button == PointerEventData.InputButton.Right)
    //     {
    //         inventorySwap.PerformSlotRightInteraction(this.gameObject);
    //     }
    // }
    //
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     inventoryDescription.SlotDescriptionStart(this.gameObject);
    // }
    //
    // public void OnPointerMove(PointerEventData eventData)
    // {
    //     inventoryDescription.SlotDescriptionUpdate(this.gameObject);
    // }
    //
    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     inventoryDescription.SlotDescriptionExit();
    // }
}
