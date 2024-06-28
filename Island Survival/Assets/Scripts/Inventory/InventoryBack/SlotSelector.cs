using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SlotSelector : MonoBehaviour
{
    [SerializeField] InventoryObject inventory;

    InventoryDisplayer displayer;

    [SerializeField] int SelectedSlotIndex;

    [Header("Sprites")]
    [SerializeField] Sprite DefaultSlotSprite;
    [SerializeField] Sprite SelectedSlotSprite;

    private void Start()
    {
        displayer = GetComponent<InventoryDisplayer>();
    }

    private void OnEnable()
    {
        InputManager.Instance.Controls.UI.Slotbar.performed += OnSlotChangePerformed;
    }

    private void OnDisable()
    {
        InputManager.Instance.Controls.UI.Slotbar.performed -= OnSlotChangePerformed;
    }

    public void ChangeSlot(int slotIndex)
    {
        if(slotIndex >= 0 && slotIndex <= 8)
        {
            displayer.SelectSlotbarSlot(slotIndex);

            SelectedSlotIndex = slotIndex;
        }
        else
        {
            Debug.Log("Slot Index Error");
        }
    }

    public void SlotItemUse()
    {
        if (displayer.InventoryStatus == InventoryStatusType.InventoryClose)
        {
            InventorySlotObsolote slotObsolote = inventory.Slotbar[SelectedSlotIndex];

            if (slotObsolote.item != null && slotObsolote.item.type == ItemType.Food)
            {
                Debug.Log(slotObsolote.item.type);

                displayer.UseSlot(slotObsolote);
            }
        }   
    }

    private void OnSlotChangePerformed(InputAction.CallbackContext obj)
    {
        ChangeSlot(int.Parse(obj.control.name));
    }

}
