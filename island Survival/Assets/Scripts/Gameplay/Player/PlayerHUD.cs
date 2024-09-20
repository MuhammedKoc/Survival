using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tmn
{
    public class PlayerHUD : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Image healthBar;
       
        [SerializeField]
        private Image hungerBar;
        
        [SerializeField]
        private Image thirstBar;
        
        [Space(10)]
        
        [SerializeField]
        private GameObject staminaFrame;
        
        [SerializeField]
        private Image staminaBar;
        
        #region Singleton

        private static PlayerHUD instance;
    
        public static PlayerHUD Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log(instance.GetType().Name+"Instance is Null");
                }

                return instance;
            }

            private set {}
        }

        private void Awake()
        {
            instance = this;
        }

        #endregion

        public void UpdateHealthBar(float value)
        {
            healthBar.fillAmount = value;
        }
        
        public void UpdateHungerBar(float value)
        {
            hungerBar.fillAmount = value;
        }
        
        public void UpdateThirstBar(float value)
        {
            thirstBar.fillAmount = value;
        }


        #region Stamina

        public void UpdatesStaminaBar(float value)
        {
            staminaBar.fillAmount = value;
        }

        public void SetActiveStaminaFrame(bool value) 
        {
            staminaFrame.SetActive(value);
        }

        #endregion

    }
}