using Inventory.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.InventoryDisplay
{
    public class EffectUI : PooledObject
    {
        [SerializeField]
        private Image sprite;

        [SerializeField]
        private TMP_Text nameText;

        [SerializeField]
        private TMP_Text valueText;

        public void Init(EffectSlot effectSlot)
        {
            sprite.sprite = effectSlot.effect.Icon;
            nameText.text = effectSlot.effect.Name;

            switch (effectSlot.changeType) {
                case EffectChangeType.Increase:
                    valueText.text = "+"; break;
                
                case EffectChangeType.Decrease:
                    valueText.text = "-"; break;
            }
            
            valueText.text += effectSlot.value.ToString();
        }
    }
}