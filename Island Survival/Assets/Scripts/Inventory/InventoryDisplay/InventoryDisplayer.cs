using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory.InventoryBack;
using Inventory.InventoryDisplay;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject rootCanvas;
    
    [SerializeField] private GameObject SlotsGB;

    [SerializeField]
    private Transform inventorySlotsParent;

    [SerializeField]
    private Transform slotbarsParent;
    
    public InventoryStatusType InventoryStatus;

    public bool CanOpenInventory;

    
    #region Privates

    private List<InventorySlot> InventorySlots = new List<InventorySlot>();
    private List<InventorySlot> InventorySlotBars = new List<InventorySlot>();

    #endregion
    
    [SerializeField] Sprite ItemNullSprite;
    public void Init(List<Slot> slots)
    {
        SetSlotsList();
        UpdateInventoryUI(slots);
    }

    void Start()
    {
        InputManager.Instance.Controls.UI.Inventory.performed += ctx =>  OpenInvetory();
    }

    private void OnDestroy()
    {
        InputManager.Instance.Controls.UI.Inventory.performed += ctx =>  OpenInvetory();

    }

    public void UpdateInventoryUI(List<Slot> slots)
    {
        foreach (var slot in slots)
        {
            if (slot.index < 8)
            {
                InventorySlotBars[slot.index].Init(slot);
            }
            else
            {
                InventorySlots[slot.index-InventoryManager.Instance.SlotbarSlotCount].Init(slot);
            }
        }
    }
    
    private void SetSlotsList()
    {
        foreach (Transform slot in slotbarsParent)
        {
            InventorySlotBars.Add(slot.GetComponent<InventorySlot>());
        }
        
        foreach (Transform slot in inventorySlotsParent)
        {
            InventorySlots.Add(slot.GetComponent<InventorySlot>());
        }
    }

    public void OpenInvetory()
    {
        if (InventoryStatus == InventoryStatusType.InventoryClose || InventoryStatus == InventoryStatusType.InventoryOpen)
        {
            inventorySlotsParent.gameObject.SetActive(!inventorySlotsParent.gameObject.activeSelf);
            if(InventoryStatus == InventoryStatusType.InventoryClose) InventoryStatus = InventoryStatusType.InventoryOpen;
            else if(InventoryStatus == InventoryStatusType.InventoryOpen) InventoryStatus = InventoryStatusType.InventoryClose;
        }
    }

    public void UseSlot(InventorySlotObsolote slotObsolote)
    {   
        slotObsolote.item.Use();

        slotObsolote.Amount--;

        if (slotObsolote.Amount == 0)
        {
            slotObsolote.Clear();
        }

        // UpdateSlotBarUI();
    }

    public void SelectSlotbarSlot(int index)
    {
        InventorySlotBars[index].GetComponent<Button>().Select();
    }
    
    
    public int GetSlotIndex(InventorySlot slot)
    {
        if (InventorySlots.Contains(slot))
        {
            return InventorySlots.IndexOf(slot) + InventoryManager.Instance.SlotbarSlotCount;
        }
        else if (InventorySlotBars.Contains(slot))
        {
            return InventorySlotBars.IndexOf(slot);
        }
        else
        {
            Debug.LogError("Not Found Slot In Slot Lists");
            return -1;
        }
    }
}

public enum InventoryStatusType
{
    InventoryOpen,
    InventoryClose,
    ItemOnDrag
}

