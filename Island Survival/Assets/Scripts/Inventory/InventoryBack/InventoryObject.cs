using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "Inventory/New Inventory Object")]
public class InventoryObject : ScriptableObject
{

    public List<InventorySlotObsolote> Slotbar = new List<InventorySlotObsolote>();
    public List<InventorySlotObsolote> inventory = new List<InventorySlotObsolote>();

    
}


[System.Serializable]
public class InventorySlotObsolote
{
    public ItemObject item;
    public int Amount;

    public InventorySlotObsolote(ItemObject item, int Amount)
    {
        this.item = item;
        this.Amount = Amount;
    }

    public void Clear()
    {
        item = null;
        Amount = 0;
    }

    public void ChangeValues(ref InventorySlotObsolote value)
    {
        ItemObject tempItem = item;
        int tempAmount = Amount;

        item = value.item;
        Amount = value.Amount;

        value.item = tempItem;
        value.Amount = tempAmount;
    }
}
