using System.Collections;
using System.Collections.Generic;
using Inventory.InventoryBack;
using Inventory.InventoryDisplay;
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

    [SerializeField] DragItem OnDragItem;
    [SerializeField] DragItemUI DragObject;
    
    [SerializeField] Vector2 DragOffSet;

    private void Start()
    {
        displayer = GetComponent<InventoryDisplayer>();
        dropper = GetComponent<ItemDropper>();
        slotSelector = GetComponent<SlotSelector>();

        OnDragItem = new DragItem(null, 0);
    }

    public void SlotLeftClick(InventorySlot slot)
    {
        switch (displayer.InventoryStatus)
        {
            case InventoryStatusType.InventoryClose:
                // slotSelector.ChangeSlot(inventory.Slotbar.IndexOf(displayer.GameObjectToSlot[slot])+1); 
                break;
            case InventoryStatusType.InventoryOpen:
                ItemDrag(slot);
                break;
            case InventoryStatusType.ItemOnDrag:
                PlaceItemToSlot(slot);
                break;
        }
    }

    
    public void PerformSlotRightInteraction(InventorySlot slot)
    {
        switch (displayer.InventoryStatus)
        {
            case InventoryStatusType.InventoryOpen:
                ItemDrag(slot, 1);
                break;
            case InventoryStatusType.ItemOnDrag:
                ItemDrag(slot, 1);
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

    private void ItemDrag(InventorySlot slot)
    {
        if (slot.Item == null) return;
        
        if (OnDragItem.item == null)
        {
            OnDragItem.item = slot.Item;
            OnDragItem.amount = slot.Amount;
        }
        else
        {
            if (OnDragItem.item != slot.Item) return;
            OnDragItem.amount += slot.Amount;
        }

        InventoryManager.Instance.ChangeSlotWithItem(displayer.GetSlotIndex(slot), null, 0);
        
        SetDragItem();

        displayer.InventoryStatus = InventoryStatusType.ItemOnDrag;
    }
    
    private void ItemDrag(InventorySlot slot, int amount)
    {
        if (slot.Item == null) return;
        
        if (OnDragItem.item == null)
        {
            OnDragItem.item = slot.Item;
            OnDragItem.amount = amount;
        }
        else
        {
            if (OnDragItem.item == slot.Item)
                OnDragItem.amount += amount;
        }

        if (OnDragItem.item != slot.Item) return;
        
        if (slot.Amount - amount > 0)
            InventoryManager.Instance.ChangeSlotWithItem(displayer.GetSlotIndex(slot), slot.Item, slot.Amount-amount);
        else
            InventoryManager.Instance.ChangeSlotWithItem(displayer.GetSlotIndex(slot), null, 0);
        
        SetDragItem();
        displayer.InventoryStatus = InventoryStatusType.ItemOnDrag;
    }

    private void PlaceItemToSlot(InventorySlot slot)
    {
        if(slot.Item == null)
        {
            Debug.Log("DragClose");

            DragObject.gameObject.SetActive(false);
            
            InventoryManager.Instance.ChangeSlotWithItem(displayer.GetSlotIndex(slot), OnDragItem.item, OnDragItem.amount);

            OnDragItem.Clear();

            displayer.InventoryStatus = InventoryStatusType.InventoryOpen;
        }
        else
        {
            if(OnDragItem.item == slot.Item)
            {
                if (slot.Item.stackable && slot.Item.stacksize > slot.Amount + OnDragItem.amount)
                {
                    InventoryManager.Instance.ChangeSlotWithItem(displayer.GetSlotIndex(slot), slot.Item, slot.Amount+OnDragItem.amount);
                }
                else return;

                DragObject.gameObject.SetActive(false);

                OnDragItem.Clear();

                displayer.InventoryStatus = InventoryStatusType.InventoryOpen;
            }
            else
            {
                Slot tempSlot = new Slot(-1, slot.Item, slot.Amount);
                
                InventoryManager.Instance.ChangeSlotWithItem(displayer.GetSlotIndex(slot), OnDragItem.item, OnDragItem.amount);
                
                OnDragItem = new DragItem(tempSlot.item, tempSlot.Amount);
                
                SetDragItem();
            }
        }
    }

    void SetDragItem()
    {
        DragObject.gameObject.SetActive(true);
        DragObject.ChangeUI(OnDragItem.item.icon, OnDragItem.amount);
    }

    public void DropItemOnDrag()
    {
        if (OnDragItem.item == null) return;
        // dropper.ItemDrop(OnDragItem.item, OnDragItem.Amount);
        // OnDragItem.Clear();
        // DragObject.SetActive(false);
        //
        // displayer.InventoryStatus = InventoryStatusType.InventoryOpen;
    }
}


