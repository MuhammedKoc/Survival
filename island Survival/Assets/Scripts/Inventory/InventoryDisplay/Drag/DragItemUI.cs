using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.InventoryDisplay
{
    public class DragItemUI : MonoBehaviour
    {
        [SerializeField]
        private Image itemSprite;

        [SerializeField]
        private TMP_Text amountText;

        public void ChangeUI(Sprite sprite, int amount)
        {
            itemSprite.sprite = sprite;

            if (amount > 1)
            {
                amountText.gameObject.SetActive(true);
                amountText.text = amount.ToString();
            }
            else
            {
                amountText.gameObject.SetActive(false);
            }
        }
    }
}