using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.InventoryBack;
using UnityEngine;
using Inventory.InventoryDisplay;
using UnityEngine.InputSystem;
using TMPro;
using MyBox;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField]
    private RectTransform descriptionObject;

    [SerializeField]
    private EffectUI effectPrefab;

    #region Values

    [Separator("Values")]
    [SerializeField]
    private Vector2 mouseOffSet;

    [SerializeField]
    private Vector2 mouseOffSetOnDrag;

    [Space(5)]
    
    [SerializeField]
    private Vector2 mouseOffSetOnSlotBar;
    
    [Space(10)]
    [SerializeField]
    private Vector2 pivotOnInventory;

    [SerializeField]
    private Vector2 pivotOnSlotbar;

    #endregion

    #region References

    [Separator("References")]
    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text typeText;

    [SerializeField]
    private TMP_Text descriptionText;

    [SerializeField]
    private Transform effectsParentTransform;

    #endregion

    #region Privates

    private PointerEventData pointerEventData;

    private ItemObject descriptionItem;

    #endregion

    private void Awake()
    {
        pointerEventData = new PointerEventData(EventSystem.current);
    }

    public void SlotDescriptionStart(InventorySlot slot)
    {
        if (InventoryManager.Instance.Displayer.InventoryStatus == InventoryStatusType.InventoryClose) return;
        if (slot.Item == null) return;

        ItemObject item = slot.Item;
        descriptionItem = item;

        descriptionObject.gameObject.SetActive(true);

        nameText.text = item.localizedName.GetLocalizedString();
        typeText.text = item.GetLocalizedType();
        descriptionText.text = item.localizedDescription.GetLocalizedString();

        LayoutRebuilder.ForceRebuildLayoutImmediate(descriptionObject);
        descriptionObject.ForceUpdateRectTransforms();

        SetPivotDescription(slot.SlotType);

        if (item is FoodObject food) SetEffectsUI(food);
    }

    //StateMachine yapıldığında stateUpdate'de olucak
    private void Update()
    {
        if (InventoryManager.Instance.Displayer.InventoryStatus == InventoryStatusType.InventoryClose) return;
        
        pointerEventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        if (results.Count > 0)
        {
            foreach (var r in results)
            {
                if (r.gameObject.TryGetComponent<InventorySlot>(out InventorySlot slot))
                {
                    CheckItems(slot);
                    
                    if (descriptionItem != null)
                        UpdateDescriptionPos(slot);
                }
            }
        }
    }

    public void SlotDescriptionExit()
    {
        descriptionItem = null;

        nameText.text = string.Empty;
        typeText.text = string.Empty;
        descriptionText.text = string.Empty;

        LayoutRebuilder.ForceRebuildLayoutImmediate(descriptionObject);
        foreach (Transform effect in effectsParentTransform)
        {
            effect.GetComponent<EffectUI>().ReturnToPool();
        }

        descriptionObject.gameObject.SetActive(false);
    }

    private void SetEffectsUI(FoodObject food)
    {
        foreach (var effectSlot in food.Effects)
        {
            EffectUI effectUI = (EffectUI)ObjectPool.Instance.Get(effectPrefab, effectsParentTransform);

            effectUI.Init(effectSlot);
        }
    }

    public void SetPivotDescription(SlotType type)
    {
        switch (type)
        {
            case SlotType.Inventory:
                descriptionObject.pivot = pivotOnInventory;
                break;
            case SlotType.Slotbar:
                descriptionObject.pivot = pivotOnSlotbar;
                break;
        }
    }

    private void CheckItems(InventorySlot slot)
    {
        if (slot.Item != null && descriptionItem == null)
        {
            SlotDescriptionStart(slot);
        } 
        else if (descriptionItem != null && slot.Item != null && slot.Item != descriptionItem)
        {
            SlotDescriptionExit();

            SlotDescriptionStart(slot);
        }
        else if (slot.Item == null)
            SlotDescriptionExit();
    }

    private void UpdateDescriptionPos(InventorySlot slot)
    {
        Vector2 offSet = Vector2.zero;
        switch (slot.SlotType)
        {
            case SlotType.Inventory:
                offSet = (InventoryManager.Instance.Displayer.InventoryStatus ==
                          InventoryStatusType.ItemOnDrag)
                    ? mouseOffSetOnDrag
                    : mouseOffSet;
                break;
            case SlotType.Slotbar:
                offSet = mouseOffSetOnSlotBar;
                break;
        }

        descriptionObject.transform.position = Mouse.current.position.ReadValue() + offSet;
    }
}