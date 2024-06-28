using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.InventoryDisplay.Description
{
    public class EffectUI : MonoBehaviour
    {
        [SerializeField]
        private Image sprite;

        [SerializeField]
        private TMP_Text nameText;

        public void Init(Effect effect)
        {
            sprite.sprite = effect.Icon;
            nameText.text = effect.Name;
        }
    }
}