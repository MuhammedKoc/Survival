using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventorySwapper : MonoBehaviour
{
    InventoryDisplayer displayer;
    ItemDropper dropper;
    SlotSelector slotSelector;

    [SerializeField] InventoryObject inventory;

    [SerializeField] InventorySlot OnDragItem;

    [SerializeField] GameObject DragObject;
    [SerializeField] Vector2 DragOffSet;

    private void Start()
    {
        displayer = GetComponent<InventoryDisplayer>();
        dropper = GetComponent<ItemDropper>();
        slotSelector = GetComponent<SlotSelector>();
    }

    public void PerformSlotLeftInteraction(GameObject slotGB)
    {
        switch (displayer.InventoryStatus)
        {
            case InventoryStatusType.InventoryClose:
                slotSelector.ChangeSlot(inventory.Slotbar.IndexOf(displayer.GameObjectToSlot[slotGB])+1); 
                break;
            case InventoryStatusType.InventoryOpen:
                InventorySlot slot = displayer.GameObjectToSlot[slotGB];
                ItemDrag(slot, slot.Amount);
                break;
            case InventoryStatusType.ItemOnDrag:
                PlaceItemToSlot(displayer.GameObjectToSlot[slotGB]);
                break;
        }
    }

 

    public void PerformSlotRightInteraction(GameObject slotGB)
    {
        switch (displayer.InventoryStatus)
        {
            case InventoryStatusType.InventoryOpen:
                ItemDrag(displayer.GameObjectToSlot[slotGB], 1);
                break;
            case InventoryStatusType.ItemOnDrag:
                ItemDrag(displayer.GameObjectToSlot[slotGB], 1);
                break;
        }
    }

    private void Update()
    {
        switch (displayer.InventoryStatus)
        {
            case InventoryStatusType.ItemOnDrag:
                DragObject.transform.position = Mouse.current.position.ReadValue() + DragOffSet;
                break;
        }
    }

    private void ItemDrag(InventorySlot slot,int Amount)
    {
        if (slot.item == null) return;

        if (OnDragItem.item == null)
        {
            OnDragItem.item = slot.item;
            OnDragItem.Amount += Amount;
        }
        else
        {
            if (OnDragItem.item != slot.item) return;
            OnDragItem.Amount += Amount;
        }

        if (slot.Amount - Amount == 0) slot.Clear();
        else slot.Amount -= Amount;

        displayer.UpdateSlotUI(slot);

        SetDragItem();

        displayer.InventoryStatus = InventoryStatusType.ItemOnDrag;
    }

    private void PlaceItemToSlot(InventorySlot slot)
    {
        if(slot.item == null)
        {
            Debug.Log("DragClose");

            DragObject.SetActive(false);
            
            slot.item = OnDragItem.item;
            slot.Amount = OnDragItem.Amount;

            displayer.UpdateSlotUI(slot);

            OnDragItem.Clear();

            displayer.InventoryStatus = InventoryStatusType.InventoryOpen;
        }
        else
        {
            if(OnDragItem.item == slot.item)
            {
                if (slot.item.stackable && slot.item.stacksize >= slot.Amount + OnDragItem.Amount)
                {
                    slot.Amount += OnDragItem.Amount;
                }
                else return;

                DragObject.SetActive(false);

                OnDragItem.Clear();

                displayer.InventoryStatus = InventoryStatusType.InventoryOpen;
            }
            else
            {
                slot.ChangeValues(ref OnDragItem);
                
                SetDragItem();
            }
        }


        
        displayer.UpdateSlotUI(slot);
    }

    void SetDragItem()
    {
        DragObject.SetActive(true);
        DragObject.transform.GetChild(0).GetComponent<Image>().sprite = OnDragItem.item.icon;
        DragObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = OnDragItem.Amount.ToString();
    }

    public void DropItemOnDrag()
    {
        if (OnDragItem.item == null) return;
        dropper.ItemDrop(OnDragItem.item, OnDragItem.Amount);
        OnDragItem.Clear();
        DragObject.SetActive(false);

        displayer.InventoryStatus = InventoryStatusType.InventoryOpen;
    }
}


