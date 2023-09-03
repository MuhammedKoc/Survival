using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotSelector : MonoBehaviour
{
    [SerializeField] InventoryObject inventory;

    InventoryDisplayer displayer;

    [SerializeField] int SelectedSlotIndex;

    [Header("Sprites")]
    [SerializeField] Sprite DefaultSlotSprite;
    [SerializeField] Sprite SelectedSlotSprite;

    [SerializeField] InputManager input;

    private void Start()
    {
        displayer = GetComponent<InventoryDisplayer>();
    }

    private void OnEnable()
    {
        input.SlotChange += ChangeSlot;
    }

    private void OnDisable()
    {
        input.SlotChange -= ChangeSlot;
    }

    public void ChangeSlot(int slotIndex)
    {
        if(slotIndex >= 1 && slotIndex <= 8)
        {
            if(SelectedSlotIndex >= 0 && SelectedSlotIndex != slotIndex-1)
            {
                Debug.Log("selected" + SelectedSlotIndex);
                displayer.SlotToGameObject[inventory.Slotbar[SelectedSlotIndex]].GetComponent<Image>().sprite = DefaultSlotSprite;
            }

            displayer.SlotToGameObject[inventory.Slotbar[slotIndex-1]].GetComponent<Image>().sprite = SelectedSlotSprite;

            SelectedSlotIndex = slotIndex-1;
        }
        else
        {
            Debug.Log("Slot Index Error");
        }
    }

}
