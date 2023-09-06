using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField] InventoryObject inventory;

    [SerializeField] GameObject SlotsGB;

    [SerializeField] List<GameObject> InventorySlots = new List<GameObject>();
    [SerializeField] List<GameObject> Slotbars = new List<GameObject>();

    public Dictionary<InventorySlot, GameObject> SlotToGameObject = new Dictionary<InventorySlot, GameObject>();

    public Dictionary<GameObject, InventorySlot> GameObjectToSlot = new Dictionary<GameObject, InventorySlot>();

    public InventoryStatusType InventoryStatus;

    public bool CanOpenInventory;

    [SerializeField] Sprite ItemNullSprite;
    private void Awake()
    {
        SetLists();


    }

    void Start()
    {
        FillDictionarys();
        SetSlots();
    }

    private void OnEnable()
    {
        inventory.SlotBarAction += UpdateSlotUI;
        inventory.InventorySlotAction += UpdateSlotUI;
    }

    private void OnDisable()
    {
        inventory.SlotBarAction -= UpdateSlotUI;
        inventory.InventorySlotAction -= UpdateSlotUI;
    }

    public void UpdateSlotUI(InventorySlot slot)
    {
        GameObject SlotGameObject = SlotToGameObject[slot];

        if (slot.item != null)
        {
            SlotGameObject.transform.GetChild(0).GetComponent<Image>().sprite = slot.item.icon;
            SlotGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = slot.Amount.ToString();
        }
        else
        {
            SlotGameObject.transform.GetChild(0).GetComponent<Image>().sprite = ItemNullSprite;
            SlotGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = null;
        }
    }

    void SetLists()
    {
        for (int i = 0; i < this.transform.GetChild(0).childCount; i++)
        {
            InventorySlots.Add(this.transform.GetChild(0).GetChild(i).gameObject);
        }
        for (int i = 0; i < this.transform.GetChild(1).childCount; i++)
        {
            Slotbars.Add(this.transform.GetChild(1).GetChild(i).gameObject);
        }

    }

    void FillDictionarys()
    {
        FillDictionaryFromTheList<GameObject, InventorySlot>(GameObjectToSlot, Slotbars.ToList(), inventory.Slotbar.ToList());
        
        FillDictionaryFromTheList<GameObject, InventorySlot>(GameObjectToSlot, InventorySlots.ToList(), inventory.inventory.ToList());
        
        FillDictionaryFromTheList<InventorySlot, GameObject>(SlotToGameObject, inventory.Slotbar.ToList(), Slotbars.ToList());

        FillDictionaryFromTheList<InventorySlot, GameObject>(SlotToGameObject, inventory.inventory.ToList(), InventorySlots.ToList());
    }

    void SetSlots()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (inventory.inventory[i].item != null)
            {
                UpdateSlotUI(inventory.inventory[i]);
            }

        }
        for (int i = 0; i < Slotbars.Count; i++)
        {
            if (inventory.Slotbar[i].item != null)
            {
                UpdateSlotUI(inventory.Slotbar[i]);
            }
        }

    }

    public void OpenInvetory()
    {
        if (InventoryStatus == InventoryStatusType.InventoryClose || InventoryStatus == InventoryStatusType.InventoryOpen)
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
            if(InventoryStatus == InventoryStatusType.InventoryClose) InventoryStatus = InventoryStatusType.InventoryOpen;
            else if(InventoryStatus == InventoryStatusType.InventoryOpen) InventoryStatus = InventoryStatusType.InventoryClose;
        }
    }

    private void FillDictionaryFromTheList<TKey, TValue>(Dictionary<TKey, TValue> dict, List<TKey> List1, List<TValue> List2)
    {
        for (int i = 0; i < List1.Count; i++)
        {
            dict.Add(List1[i], List2[i]);
        }
    }

    public void UseSlot(InventorySlot slot)
    {   
        slot.item.Use();

        slot.Amount--;

        if (slot.Amount == 0)
        {
            slot.Clear();
        }

        UpdateSlotUI(slot);
    }
}

public enum InventoryStatusType
{
    InventoryOpen,
    InventoryClose,
    ItemOnDrag
}

