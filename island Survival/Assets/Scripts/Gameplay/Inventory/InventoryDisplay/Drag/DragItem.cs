namespace Inventory.InventoryDisplay
{
    public class DragItem
    {
        public ItemObject item;
        public int amount;

        public DragItem(ItemObject item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void Clear()
        {
            item = null;
            amount = 0;
        }
    }
}