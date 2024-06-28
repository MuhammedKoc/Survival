using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Inventory.InventoryDisplay;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using MyBox;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Vector2 DescriptionMouseOffSet;
    
    [SerializeField]
    private GameObject DescriptionObject;

    [SerializeField]
    private GameObject effectPrefab;

    [Separator("Values")]
    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text typeText;
    
    [SerializeField]
    private TMP_Text descriptionText;
    
    [SerializeField]
    private Transform effectsParentTransform;

    public void SlotDescriptionStart(InventorySlot slot)
    {
        // if (inventoryDisplayer.GameObjectToSlot[slot].item == null) return;

        Debug.Log("Start");
        
        ItemObject item = slot.Item;

        nameText.text = item.ItemName;
        typeText.text = item.type.ToString();
        descriptionText.text = item.Description;

        if (item is FoodObject food)
        {
            foreach (Transform child in effectsParentTransform) { Destroy(child); }

            foreach (var effect in food.Effects)
            {
                
            }
        }
        
        // for (int i = 0; i < EffectsTransform.childCount; i++)
        // {
        //     EffectsTransform.GetChild(i).gameObject.SetActive(false);
        // }
        
        DescriptionObject.SetActive(true);

        // SetDescriptionText(item);
        //
        // SetUISizeAndPosition(item);
    }

    public void SlotDescriptionUpdate(InventorySlot slot)
    {
        // if (inventoryDisplayer.GameObjectToSlot[slot].item == null) return;

        Debug.Log("Move");

        DescriptionObject.transform.position = Mouse.current.position.ReadValue() + DescriptionMouseOffSet;
    }

    public void SlotDescriptionExit()
    {
        Debug.Log("Exit");

        DescriptionObject.SetActive(false);

        RectTransform DescriptionTransform = DescriptionObject.GetComponent<RectTransform>();
        DescriptionTransform.sizeDelta = new Vector2(DescriptionTransform.sizeDelta.x, 19);
    }
    private void SetEffectsUI(ItemObject item)
    {
        FoodObject foodItem = (FoodObject)item;
        
        // for (int i = 0; i < foodItem.Effects.Length; i++)
        // {
        //    Transform effectTransform = EffectsTransform.GetChild(i);
        //    EffectSlot effectSlot = foodItem.Effects[i];
        //    
        //    string EffectValue = (effectSlot.value >= 0) ? $"+{effectSlot.value}" : effectSlot.value.ToString();
        //    
        //    effectTransform.Find("Icon").GetComponent<Image>().sprite = effectSlot.effect.Icon;
        //    
        //    effectTransform.Find("Text").GetComponent<TextMeshProUGUI>().text = EffectValue+ " " + effectSlot.effect.Name;
        //    
        //    effectTransform.gameObject.SetActive(true);
        // }
        //
        // DescriptionTransform.sizeDelta = new Vector2(DescriptionTransform.sizeDelta.x, DescriptionTransform.sizeDelta.x + (foodItem.Effects.Length * 1f));
    }
}

