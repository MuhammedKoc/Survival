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
    
    [Space(10)]
    [SerializeField]
    private GameObject inventoryPanel;

    [Space(10)]
    
    [SerializeField]
    private Transform inventorySlotsParent;
    
    [SerializeField]
    private Transform slotbarsParent;
    
    [HideInInspector] public InventoryStatusType InventoryStatus;
    
    #region Privates

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    private List<InventorySlot> slotBars = new List<InventorySlot>();

    #endregion
    
    public void Init(List<Slot> slots)
    {
        SetSlotsList();
        UpdateInventoryUI(slots);
    }

    void Start()
    {
        InputManager.Controls.Player.OpenInventory.performed += ctx =>  OpenInventory();
        InputManager.Controls.UI.CloseInventory.performed += ctx =>  CloseInvetory();
    }

    private void OnDestroy()
    {
        InputManager.Controls.Player.OpenInventory.performed -= ctx =>  OpenInventory();
        InputManager.Controls.UI.CloseInventory.performed -= ctx =>  CloseInvetory();
    }

    public void UpdateInventoryUI(List<Slot> slots)
    {
        foreach (var slot in slots)
        {
            if (slot.index < 8)
            {
                slotBars[slot.index].Init(slot);
            }
            else
            {
                inventorySlots[slot.index-InventoryManager.Instance.SlotbarSlotCount].Init(slot);
            }
        }
    }
    
    private void SetSlotsList()
    {
        foreach (Transform slot in slotbarsParent)
        {
            slotBars.Add(slot.GetComponent<InventorySlot>());
        }
        
        foreach (Transform slot in inventorySlotsParent)
        {
            inventorySlots.Add(slot.GetComponent<InventorySlot>());
        }
    }

    public void CloseInvetory()
    {
        if (InventoryStatus != InventoryStatusType.InventoryOpen) return;
        
        inventoryPanel.SetActive(false);
        InventoryStatus = InventoryStatusType.InventoryClose;

        InventoryManager.Instance.Description.SlotDescriptionExit();
        
        InputManager.Instance.DisableAllMaps();
        InputManager.Controls.Player.Enable();
    }

    public void OpenInventory()
    {
        if (InventoryStatus != InventoryStatusType.InventoryClose) return;
        
        inventoryPanel.SetActive(true);
        InventoryStatus = InventoryStatusType.InventoryOpen;
        
        InputManager.Instance.DisableAllMaps();
        InputManager.Controls.UI.Enable();
    }

    public void SelectSlotbarSlotByIndex(int index)
    {
        slotBars[index].GetComponent<Button>().Select();
    }
    
    public int GetSlotIndexBySlot(InventorySlot slot)
    {
        if (inventorySlots.Contains(slot))
        {
            return inventorySlots.IndexOf(slot) + InventoryManager.Instance.SlotbarSlotCount;
        }
        else if (slotBars.Contains(slot))
        {
            return slotBars.IndexOf(slot);
        }
        else
        {
            Debug.LogError("Not Found Slot In Slot Lists");
            return -1;
        }
    }
    
    public InventorySlot GetSlotByIndex(int index)
    {
        if (index < 0) return null;

        if (index < InventoryManager.Instance.SlotbarSlotCount) {
            return slotBars[index];
        }
        else {
            return inventorySlots[index];
        }
    }
}

public enum InventoryStatusType
{
    InventoryOpen,
    InventoryClose,
    ItemOnDrag
}

