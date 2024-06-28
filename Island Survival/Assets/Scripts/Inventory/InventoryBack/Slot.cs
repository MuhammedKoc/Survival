using System;

namespace Inventory.InventoryBack
{
    [Serializable]
    public class Slot
    {
        public int index;
        public ItemObject item;
        public int Amount;

        public Slot(int index,ItemObject item, int Amount)
        {
            this.index = index;
            this.item = item;
            this.Amount = Amount;
        }

        public void SetValues(ItemObject item, int Amount)
        {
            this.item = item;
            this.Amount = Amount;
        }

        public void Clear()
        {
            item = null;
            Amount = 0;
        }

        public void ChangeValues(ref InventorySlotObsolote value)
        {
            ItemObject tempItem = item;
            int tempAmount = Amount;

            item = value.item;
            Amount = value.Amount;

            value.item = tempItem;
            value.Amount = tempAmount;
        }
    }
}