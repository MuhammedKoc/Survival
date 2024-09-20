using Inventory.InventoryBack;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.InventoryDisplay
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private SlotType slotType;
        public SlotType SlotType => slotType;
        
        [SerializeField]
        private InventorySlotUI ui;

        private ItemObject item;
        public ItemObject Item => item;
        
        private int amount;
        public int Amount => amount;
        
        public void Init(Slot slot)
        {
            this.item = slot.item;
            this.amount = slot.Amount;
            
            ui.Init(slot);
        }

        public void Clear()
        {
            item = null;
            amount = 0;

            ui.Clear();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                InventoryManager.Instance.Swapper.SlotLeftClick(this);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                InventoryManager.Instance.Swapper.PerformSlotRightInteraction(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            InventoryManager.Instance.Description.SlotDescriptionStart(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InventoryManager.Instance.Description.SlotDescriptionExit();
        }

        public void Select()
        {
            if (slotType == SlotType.Slotbar)
            {
                ui.Select();
            }
        }

        public void Unselect()
        {
            ui.UnSelect();
        }
    }
}