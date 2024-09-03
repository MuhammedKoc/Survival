using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Notification
{
    public class NotifyUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        [SerializeField]
        private Image iconImage;

        [SerializeField]
        private TMP_Text text;
        
        [SerializeField]
        private AnimationCurve iconTweenCurve;

        
        protected Action onComplete;
        
        //Privates
        private NotifyData notifyData;
        
        private Tween durationTween;
        private Tween transparentTween;
        private Tween iconTween;


        public void Init(NotifyData notifyData, Action onComplete)
        {
            this.notifyData = notifyData;
            this.onComplete = onComplete;

            UpdateDefaultValues(this.notifyData.icon, this.notifyData.localizedText.GetLocalizedString());

            TweenAnimations();
        }
        
        protected void TweenAnimations()
        {
            if(durationTween != null) durationTween.Kill();
            if(transparentTween != null) transparentTween.Kill();

            canvasGroup.alpha = 1;
            iconImage.transform.localScale = Vector3.one;
            
            iconTween = iconImage.transform.DOScale(new Vector3(1.2f,1.2f,1f), NotifyConstantValues.iconTweenDuration)
                .OnComplete(() =>
                {
                    iconImage.transform.DOScale(Vector3.one, NotifyConstantValues.iconTweenDuration);
                });
            
            durationTween = DOVirtual.DelayedCall(NotifyConstantValues.notifyDuration, () =>
            {
                transparentTween = canvasGroup.DOFade(0f ,NotifyConstantValues.transparentTweenDuration)
                    .SetEase(NotifyConstantValues.transparentEase).OnComplete(() =>
                    {
                        onComplete?.Invoke();
                        
                        this.gameObject.SetActive(false);
                    });
            });
        }

        protected void UpdateDefaultValues(Sprite icon, string message)
        {
            iconImage.sprite = icon;
            text.text = message;
        }
        
    }
}