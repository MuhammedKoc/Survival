using Inventory.InventoryBack;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.InventoryDisplay
{
    public class InventorySlotUI : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField]
        private Image sprite;

        [SerializeField]
        private TMP_Text amountText;

        [SerializeField]
        private Image slotSprite;
        
        //TODO: Bu Spriteların her slotta olması mantıklı mı bunu araştır.
        [SerializeField]
        private Sprite defaultSlotSprite;

        [SerializeField]
        private Sprite selectedSlotSprite;

        public void Init(Slot slot)
        {
            if (slot.item != null)
            {
                sprite.sprite = slot.item.icon;
                sprite.gameObject.SetActive(true);
            }
            else
            {
                Clear();
                return;
            }
            
            if (slot.Amount > 1)
            {
                amountText.text = slot.Amount.ToString();
                amountText.gameObject.SetActive(true);
            }
            else
            {
                amountText.gameObject.SetActive(false);
            }
        }

        public void Clear()
        {
            sprite.gameObject.SetActive(false);
            amountText.gameObject.SetActive(false);
        }
        
        public void OnSelect(BaseEventData eventData)
        {
            slotSprite.sprite = selectedSlotSprite;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            slotSprite.sprite = defaultSlotSprite;

        }
    }
}