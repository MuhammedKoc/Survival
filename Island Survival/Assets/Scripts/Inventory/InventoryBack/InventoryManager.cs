using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory.InventoryBack;
using Inventory.InventoryDisplay;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   [SerializeField]
   private InventoryDisplayer displayer;

   [SerializeField]
   private InventorySwapper swapper;
   public InventorySwapper Swapper => swapper;
   
   [SerializeField]
   private InventoryDescription description;
   public InventoryDescription Description => description;

   [SerializeField]
   private int inventorySlotCount;
   public int InventorySlotCount => inventorySlotCount;

   [SerializeField]
   private int slotbarSlotCount;
   public int SlotbarSlotCount => slotbarSlotCount;
   
   private List<Slot> slots;

   private List<Slot> changedSlots = new List<Slot>();
   
   #region Singleton

   private static InventoryManager instance = null;
   
   public static InventoryManager Instance
   {
       get
       {
           if (instance == null)
           {
               Debug.Log(instance.GetType().Name+"Instance is Null");
           }

           return instance;
       }
   }

   private void Awake()
   {
       instance = this;
       
       Init();
   }

   #endregion

   #region Show & Hide

   [SerializeField]
   private GameObject rootCanvas;
   
   private void Show()
   {
       rootCanvas.SetActive(true);
   }

   private void Hide()
   {
       rootCanvas.SetActive(false);
   }

   #endregion
   
   private void Start()
   {
       // InputManager.Instance.Controls.Player.UseSlot += 
   }

   public void Init()
   {
       slots = new List<Slot>();
       
       ClearSlots();
       
       displayer.Init(slots);
   }

   public bool AddItem(ItemObject item, int amount, out int remainAmount)
    {
        int _TempAmount = amount;
        bool resultBool = false;
        if (item.stackable)
        {
            StackableItemAddAmount(item, amount, ref _TempAmount);

            if (_TempAmount > 0 && EmptySlotCount > 0)
            {
                StackableItemAdd(item, ref _TempAmount);
            }
            
            UpdateUI();
        }
        else
        {
            if (amount > 1)
            {
                Debug.LogError("Non-stackable items cannot have more than 1 amount");

                remainAmount = amount;
                return false;
            }
            
            if (EmptySlotCount > 0)
            {
                SetEmptySlot(item, _TempAmount);

                UpdateUI();
                
                remainAmount = 0;
                return true;
            }
        }

        if (_TempAmount > 0) resultBool = false;
        else if (_TempAmount == 0) resultBool = true;

        remainAmount = _TempAmount;
        return resultBool;
    }
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;

            foreach (var slot in slots)
            {
                if (slot.item == null)
                {
                    counter++;
                }
            }

            return counter;
        }
    }

    private void SetEmptySlot(ItemObject item, int amount)
    {
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.SetValues(item, amount);
                changedSlots.Add(slot);
                break;
            }
        }
    }

    public bool CheckSpaceForItem(ItemObject item, int amount)
    {
        if (EmptySlotCount > 0)
            return true;

        if (item.stackable)
        {
            foreach (var slot in slots)
            {
                if (item == slot.item && slot.Amount + amount < item.stacksize)
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
        slots = new List<Slot>();
        
        for (int i = 0; i < inventorySlotCount+slotbarSlotCount; i++)
        {
            slots.Add(new Slot(i, null, 0));
        }
    }

    private bool StackableItemAddAmount(ItemObject _item, int _Amount, ref int Temp_Amount)
    {
        foreach (var slot in slots)
        {
            if (_item == slot.item)
            {
                if (Temp_Amount == 0) return true;
                
                if (slot.Amount + Temp_Amount <= _item.stacksize)
                {          
                    slot.Amount += Temp_Amount;
                    Temp_Amount -= Temp_Amount;
                    
                    changedSlots.Add(slot);
                    
                    return true;
                }
                else
                {
                    int test = _item.stacksize - slot.Amount;
                    slot.Amount += test;
                    changedSlots.Add(slot);
                    
                    Temp_Amount -= test;
                }
                
            }
        }
        return false;
    }

    private bool StackableItemAdd(ItemObject _item, ref int _TempAmount)
    {
        int TempAmount = _TempAmount;
        
        if (_TempAmount > _item.stacksize)
        {
            int requiredSlot = (int)Mathf.Ceil((float)_TempAmount / _item.stacksize);
            
            for (int i = 1; i < requiredSlot; i++)
            {
                if (EmptySlotCount > 0)
                {
                    if (i != requiredSlot)
                    {
                        SetEmptySlot(_item, _item.stacksize);
                        TempAmount -= _item.stacksize;
                    }
                    else
                    {
                        SetEmptySlot(_item, _TempAmount);
                        TempAmount = 0;
                        return true;
                    }
                }
                else break;
            }
        }
        else
        {
            SetEmptySlot(_item, _TempAmount);
            TempAmount -= _TempAmount;
        }
       

        _TempAmount = TempAmount;
        return false;
    }

    public void ChangeSlotWithItem(int slotIndex, ItemObject item, int amount)
    {

        slots[slotIndex].SetValues(item, amount);
        
        changedSlots.Add(slots[slotIndex]);
        UpdateUI();
    }

    private void UpdateUI()
    {       
        Debug.Log(changedSlots.Count);
        displayer.UpdateInventoryUI(changedSlots);
        changedSlots = new List<Slot>();
    }
}
