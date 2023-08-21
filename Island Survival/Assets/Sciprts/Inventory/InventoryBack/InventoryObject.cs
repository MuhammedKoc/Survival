using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="InventoryObject",menuName ="Inventory/New Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventory;

    public bool AddItem(ItemObject item, int amount)
    {
        if (item.stackable)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (item == inventory[i].item && inventory[i].Amount< item.stacksize)
                {
                    inventory[i].Amount += amount;
                    return true;                   
                }
            }
        }

        if (EmptySlotCount <= 0) 
            return false;
        
        SetEmptySlot(item, amount);
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].item ==null)
                {
                    counter++;
                }
            }
            return counter;
        }
    }

    private void SetEmptySlot(ItemObject item, int amount)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == null)
            {
                inventory[i] = new InventorySlot(item, amount);
                break;
            }

        }
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
}
