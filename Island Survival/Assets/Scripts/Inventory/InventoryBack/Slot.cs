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
    }
}