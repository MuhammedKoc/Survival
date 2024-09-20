using System.Collections;
using System.Collections.Generic;
using Inventory.InventoryDisplay;
using Inventory.Item;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Slotbar : MonoBehaviour
{
    [SerializeField]
    private InventoryDisplayer displayer;
    
    //Privates
    private InventorySlot selectedSlot;

    private void Start()
    {
        InputManager.Controls.Player.Slotbar.performed += OnSlotChangePerformed;
        InputManager.Controls.Player.MouseRightClick.performed += OnSlotUse;
    }

    private void OnDestroy()
    {
        InputManager.Controls.Player.Slotbar.performed -= OnSlotChangePerformed;
        InputManager.Controls.Player.MouseRightClick.performed -= OnSlotUse;
    }

    public void ChangeSlot(int slotIndex)
    {
        if(slotIndex >= 0 && slotIndex <= InventoryManager.Instance.SlotbarSlotCount)
        {
            // displayer.SelectSlotbarSlotByIndex(slotIndex);
            
            if(selectedSlot != null)
                selectedSlot.Unselect();
            
            selectedSlot = displayer.GetSlotByIndex(slotIndex);
            selectedSlot.Select();
        }
        else
        {
            Debug.Log("Slot Index Error");
        }
    }

    public void SlotItemUse()
    {
        if (displayer.InventoryStatus != InventoryStatusType.InventoryClose) return;
        
        if(selectedSlot == null) return;
        if (selectedSlot.Item is not UseableItem useableItem) return;
        
        useableItem.Use();
        if (useableItem.amountDecreaseWhenUse)
        {
            InventoryManager.Instance.ChangeSlotWithThisValues(displayer.GetSlotIndexBySlot(selectedSlot), selectedSlot.Item, selectedSlot.Amount-1);
        }
    }

    private void OnSlotChangePerformed(InputAction.CallbackContext obj)
    {
        ChangeSlot(int.Parse(obj.control.name)-1);
    }
    
    private void OnSlotUse(InputAction.CallbackContext obj)
    {
        SlotItemUse();
    }

    public void SetSelectedSlot(InventorySlot slot)
    {
        selectedSlot.Unselect();
        selectedSlot = slot;
    }
}
