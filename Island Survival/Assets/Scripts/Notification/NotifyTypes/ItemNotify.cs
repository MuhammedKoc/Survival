using System;
using UnityEngine;

namespace Notification
{
    public class ItemNotify : Notify
    {

        private ItemObject item;
        public ItemObject Item => item;
        
        private int amount;
        private ItemNotifyUI itemNotifyUI;

        private void Awake()
        {
            itemNotifyUI = (ItemNotifyUI)ui;
        }

        public void Init(ItemObject item, int amount, Action onComplete)
        {
            this.item = item;
            this.amount = amount;
            
            itemNotifyUI.Init(item, amount, onComplete);
        }

        public void AddAmount(int amount)
        {
            this.amount += amount;
            
            itemNotifyUI.Init(item, this.amount);
        }
    }
}