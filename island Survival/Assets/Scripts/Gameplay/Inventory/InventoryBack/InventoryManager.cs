using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory.InventoryBack;
using Inventory.InventoryDisplay;
using Inventory.Item;
using MyBox;
using Tmn.Data;
using Tmn.Data.DataPersistence;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    #region Side Scripts

    [Separator("Side Scripts")]
    [SerializeField]
    private InventoryDisplayer displayer;
    public InventoryDisplayer Displayer => displayer;

    [SerializeField]
    private Slotbar slotbar;
    public Slotbar Slotbar => slotbar;
    
    [SerializeField]
    private InventorySwapper swapper;
    public InventorySwapper Swapper => swapper;

    [SerializeField]
    private InventoryDescription description;
    public InventoryDescription Description => description;

    #endregion

    [Separator("Values")]
    [SerializeField] private int inventorySlotCount;
    public int InventorySlotCount => inventorySlotCount;

    [SerializeField] private int slotbarSlotCount;
    public int SlotbarSlotCount => slotbarSlotCount;

    //Privates
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
                Debug.Log(instance.GetType().Name + "Instance is Null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;

        // Init();
    }

    #endregion

    #region Show & Hide

    [SerializeField]
    private GameObject rootCanvas;

    private void Show()
    {
        
    }

    private void Hide()
    {
        
    }

    #endregion
    
    // public void Init()
    // {
    //     slots = new List<Slot>();
    //
    //     ClearSlots();
    // }

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
                SetItemToFirstEmptySlot(item, _TempAmount);

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

    private void SetItemToFirstEmptySlot(ItemObject item, int amount)
    {
        foreach (var slot in slots)
        {
            if (slot.item != null) continue;

            slot.SetValues(item, amount);
            changedSlots.Add(slot);
            break;
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

    /// <summary>
    /// This Method public for Debug Helper
    /// </summary>
    [ContextMenu("Clear Slots")]
    public void ClearSlots()
    {
        slots = new List<Slot>();

        for (int i = 0; i < inventorySlotCount + slotbarSlotCount; i++)
        {
            slots.Add(new Slot(i, null, 0));
        }
        
        displayer.Init(slots);
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
        if (_TempAmount > _item.stacksize)
        {
            int requiredSlot = Mathf.CeilToInt((float)_TempAmount / _item.stacksize);
            
            for (int i = 1; i <= requiredSlot; i++)
            {
                if (EmptySlotCount <= 0) break;
                
                if (i == requiredSlot && _TempAmount < _item.stacksize)
                {
                    SetItemToFirstEmptySlot(_item, _TempAmount);
                    _TempAmount = 0;
                    return true;
                }
                
                SetItemToFirstEmptySlot(_item, _item.stacksize);
                _TempAmount -= _item.stacksize;
            }
        }
        else
        {
            SetItemToFirstEmptySlot(_item, _TempAmount);
            _TempAmount = 0;
        }

        return false;
    }

    public void ChangeSlotWithThisValues(int slotIndex, ItemObject item, int amount)
    {
        if (amount <= 0) {
            slots[slotIndex].Clear();
        }
        else {
            slots[slotIndex].SetValues(item, amount);
        }

        changedSlots.Add(slots[slotIndex]);
        UpdateUI();
    }

    private void UpdateUI()
    {
        displayer.UpdateInventoryUI(changedSlots);
        changedSlots = new List<Slot>();
    }

    public void LoadData(GameData data)
    {
        slots = data.InventorySlots;
        
        displayer.Init(slots);
    }

    public void SaveData(ref GameData data)
    {
        data.InventorySlots = slots;
    }
}