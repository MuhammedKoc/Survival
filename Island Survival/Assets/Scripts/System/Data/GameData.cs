using System.Collections.Generic;
using System.Linq;
using Inventory.InventoryBack;

namespace Tmn.Data
{
    public class GameData
    {
        #region Inventory

        public List<Slot> InventorySlots = new Slot[32].ToList();

        #endregion

        #region Survival Status

        public float Health;
        public float Hunger;
        public float Thirst;
        
        #endregion
    }
}