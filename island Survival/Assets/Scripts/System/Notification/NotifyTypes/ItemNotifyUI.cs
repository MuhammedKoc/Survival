using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Notification
{
    public class ItemNotifyUI : NotifyUI
    {
        [SerializeField]
        private TMP_Text amountText;
        
        //Privates
        private Tween durationTween;
        private Tween transparentTween;
        private Tween iconTween;

        public void Init(ItemObject item, int amount, Action onComplete = null)
        {
            this.onComplete = onComplete;
            
            UpdateDefaultValues(item.icon, item.localizedName.GetLocalizedString());
            amountText.text = amount.ToString();
            
            TweenAnimations();
        }
    }
}