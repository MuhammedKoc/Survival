using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "Inventory/New Inventory Object")]
public class InventoryObject : ScriptableObject
{

    public List<InventorySlot> Slotbar = new List<InventorySlot>();
    public List<InventorySlot> inventory = new List<InventorySlot>();

    public event Action<InventorySlot> SlotBarAction;
    public event Action<InventorySlot> InventorySlotAction;



    public bool AddItem(ItemObject item, int amount)
    {
        int _TempAmount = amount;
        if (item.stackable)
        {
            if (StackableItemAddAmount(Slotbar, item, amount, SlotBarAction, ref _TempAmount)) return true;

            if (StackableItemAddAmount(inventory, item, amount, InventorySlotAction, ref _TempAmount)) return true;

            if (StackableItemAdd(item, ref _TempAmount)) return true;

            //if (EmptySlotCount > 0 && _TempAmount > item.stacksize)
            //{
            //    SetEmptySlot(item, _TempAmount - item.stacksize);
            //    Debug.Log("item stack add"+ _TempAmount);
            //    return AddItem(item, _TempAmount - item.stacksize);
            //}
        }

        if (EmptySlotCount <= 0)
            return false;

        SetEmptySlot(item, _TempAmount);
        return true;
    }
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;

            for (int i = 0; i < Slotbar.Count; i++)
            {
                if (Slotbar[i].item == null)
                {
                    counter++;
                }
            }

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].item == null)
                {
                    counter++;
                }
            }

            return counter;
        }
    }

    private void SetEmptySlot(ItemObject item, int amount)
    {
        for (int i = 0; i < Slotbar.Count; i++)
        {
            if (Slotbar[i].item == null)
            {
                Slotbar[i].item = item;
                Slotbar[i].Amount = amount;
                SlotBarAction.Invoke(Slotbar[i]);
                return;
            }
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == null)
            {
                inventory[i].item = item;
                inventory[i].Amount = amount;
                InventorySlotAction.Invoke(inventory[i]);
                return;
            }

        }
    }

    public bool CheckSpaceForItem(ItemObject item, int amount)
    {
        if (EmptySlotCount > 0)
            return true;

        if (item.stackable)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (item == inventory[i].item && inventory[i].Amount + amount < item.stacksize)
                {
                    return true;
                }
            }
        }

        return false;
    }

    [ContextMenu("Clear Slots")]
    private void ClearSlots()
    {
        inventory = Enumerable.Repeat(new InventorySlot(null, 0), 24).ToList();
        Slotbar = Enumerable.Repeat(new InventorySlot(null, 0), 8).ToList();
    }

    private bool StackableItemAddAmount(List<InventorySlot> slotList, ItemObject _item, int _Amount, Action<InventorySlot> _Action, ref int Temp_Amount)
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            if (_item == slotList[i].item)
            {
                if (Temp_Amount == 0) return true;
                
                if (slotList[i].Amount + Temp_Amount <= _item.stacksize)
                {          
                    slotList[i].Amount += Temp_Amount;
                    _Action?.Invoke(slotList[i]);
                    return true;
                }
                else
                {
                    int test = _item.stacksize - slotList[i].Amount;
                    slotList[i].Amount += test;
                    Temp_Amount -= test;
                    _Action?.Invoke(slotList[i]);
                }
                
            }
        }
        return false;
    }

    private bool StackableItemAdd(ItemObject _item, ref int _TempAmount)
    {
        int requiredSlot = (int)Mathf.Ceil((float)_TempAmount / _item.stacksize);
        
        if(EmptySlotCount >= requiredSlot)
        {
            int TempAmount = _TempAmount;
            for (int i = 0; i < requiredSlot; i++)
            {
                if (i != requiredSlot - 1)
                {
                    SetEmptySlot(_item, _item.stacksize);
                }
                else
                {
                    SetEmptySlot(_item, _TempAmount - (_item.stacksize*(requiredSlot-1)));
                    return true;
                }
            }
        }

        return false;
    }
}


[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int Amount;

    public InventorySlot(ItemObject item, int Amount)
    {
        this.item = item;
        this.Amount = Amount;
    }

    public void Clear()
    {
        item = null;
        Amount = 0;
    }

    public void ChangeValues(ref InventorySlot value)
    {
        ItemObject tempItem = item;
        int tempAmount = Amount;

        item = value.item;
        Amount = value.Amount;

        value.item = tempItem;
        value.Amount = tempAmount;
    }
}
