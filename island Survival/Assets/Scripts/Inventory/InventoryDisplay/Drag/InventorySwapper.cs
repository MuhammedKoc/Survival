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
    Slotbar slotbar;

    [SerializeField]
    DragItem OnDragItem;

    [SerializeField]
    DragItemUI DragObject;

    [SerializeField]
    Vector2 DragOffSet;

    private void Start()
    {
        displayer = GetComponent<InventoryDisplayer>();
        dropper = GetComponent<ItemDropper>();
        slotbar = GetComponent<Slotbar>();

        OnDragItem = new DragItem(null, 0);
    }

    public void SlotLeftClick(InventorySlot slot)
    {
        switch (displayer.InventoryStatus)
        {
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
        if(!CheckAndSetDragItem(slot, slot.Amount)) return;

        InventoryManager.Instance.ChangeSlotWithThisValues(displayer.GetSlotIndexBySlot(slot), null, 0);

        SetDragItemUI();

        displayer.InventoryStatus = InventoryStatusType.ItemOnDrag;
    }

    private void ItemDrag(InventorySlot slot, int amount)
    {
        if (slot.Item == null) return;
        if(!CheckAndSetDragItem(slot, amount)) return;
        
        if (slot.Amount - amount > 0)
        {
            InventoryManager.Instance.ChangeSlotWithThisValues(displayer.GetSlotIndexBySlot(slot), slot.Item, slot.Amount - amount);
        }
        else
        { 
            InventoryManager.Instance.ChangeSlotWithThisValues(displayer.GetSlotIndexBySlot(slot), null, 0);
        }
        SetDragItemUI();
        displayer.InventoryStatus = InventoryStatusType.ItemOnDrag;
    }

    private void PlaceItemToSlot(InventorySlot slot)
    {
        if (slot.Item == null)
        {
            ChangeSlotAmountOnItemMatch(slot, OnDragItem.amount);
            return;
        }

        if (OnDragItem.item == slot.Item && slot.Item.stackable)
        {
            if (slot.Item.stacksize >= slot.Amount + OnDragItem.amount)
            {
                ChangeSlotAmountOnItemMatch(slot, slot.Amount + OnDragItem.amount);
            }
            else
            {
                int remain = OnDragItem.amount - (slot.Item.stacksize - slot.Amount);
                InventoryManager.Instance.ChangeSlotWithThisValues(displayer.GetSlotIndexBySlot(slot), slot.Item, slot.Item.stacksize);

                OnDragItem.amount = remain;
                SetDragItemUI();
            }
        }
        else
        {
            Slot tempSlot = new Slot(-1, slot.Item, slot.Amount);
            InventoryManager.Instance.ChangeSlotWithThisValues(displayer.GetSlotIndexBySlot(slot), OnDragItem.item, OnDragItem.amount);

            OnDragItem = new DragItem(tempSlot.item, tempSlot.Amount);
            SetDragItemUI();
        }
    }

    private bool CheckAndSetDragItem(InventorySlot slot, int amount)
    {
        if (OnDragItem.item == null)
        {
            OnDragItem.item = slot.Item;
            OnDragItem.amount = amount;
        }
        else
        {
            if (OnDragItem.item != slot.Item) return false;
            
            if (!OnDragItem.item.stackable) return false;
            if (OnDragItem.amount + amount > OnDragItem.item.stacksize) return false;

            OnDragItem.amount += amount;
        }

        return true;
    }

    void SetDragItemUI()
    {
        DragObject.gameObject.SetActive(true);
        DragObject.ChangeUI(OnDragItem.item.icon, OnDragItem.amount);
    }

    // Changing the slot amount by the specified amount when the slot and ItemDrag's item are the same and closing the item drag
    private void ChangeSlotAmountOnItemMatch(InventorySlot slot, int amount)
    {
        InventoryManager.Instance.ChangeSlotWithThisValues(displayer.GetSlotIndexBySlot(slot), OnDragItem.item, amount);

        OnDragItem.Clear();
        DragObject.gameObject.SetActive(false);

        displayer.InventoryStatus = InventoryStatusType.InventoryOpen;
    }

    public void DropItemOnDragSpaceArea()
    {
        if (OnDragItem.item == null && displayer.InventoryStatus != InventoryStatusType.ItemOnDrag) return;
        ItemDropper.Instance.ItemDrop(OnDragItem.item, OnDragItem.amount);
        OnDragItem.Clear();
        DragObject.gameObject.SetActive(false);

        displayer.InventoryStatus = InventoryStatusType.InventoryOpen;
    }
}