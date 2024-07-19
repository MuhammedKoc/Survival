using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

namespace Notification
{
    public class NotificationManager : MonoBehaviour
    {
        [SerializeField]
        private Notify defaultPrefab;
        
        [SerializeField]
        private ItemNotify itemNotifyPrefab;
        
        [Space(10)]
        
        [SerializeField]
        private Transform notifiesParent;
        
        //Privates
        private List<Notify> notifies = new List<Notify>();
        private List<ItemNotify> itemNotifies = new List<ItemNotify>();
        
        #region Singleton
        
        private static NotificationManager instance = null;
        
        public static NotificationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log(instance.GetType().Name + "Instance is Null");
                }
        
                return instance;
            }
        }
        
        private void Awake()
        {
            instance = this;
        }
        
        #endregion
        
        public void ShowNotify(NotifyData data)
        {
            Notify notify = notifies.SingleOrDefault(p => p.NotifyData.type == data.type && p.gameObject.activeSelf);
        
            if (notify == null)
            {
                notify = (Notify)ObjectPool.Instance.Get(defaultPrefab, notifiesParent);
                if(!notifies.Contains(notify)) notifies.Add(notify);
            }
            
            notify.Init(data, () =>
            {
                notifies.Remove(notify);
            });
        }
        
        public void NotifyItem(ItemObject item, int amount)
        {
            ItemNotify itemN = itemNotifies.SingleOrDefault(p => p.Item == item && p.gameObject.activeSelf);

            if (itemN != null)
            {
                itemN.AddAmount(amount);
                return;
            }
            
            itemN = (ItemNotify)ObjectPool.Instance.Get(itemNotifyPrefab, notifiesParent);
            if(!itemNotifies.Contains(itemN)) itemNotifies.Add(itemN);
            
            itemN.Init(item, amount, () =>
            {
                itemNotifies.Remove(itemN);
            });
        }
    }
}