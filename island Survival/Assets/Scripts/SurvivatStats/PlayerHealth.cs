using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Instance
    
    private static PlayerHealth instance;

    public static PlayerHealth Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogWarning("HealthManager Instance Error");
            }
            return instance;
        }
    }
    #endregion
    
    [SerializeField] 
    private float maxHealth = 100;
    
    [Space(10)]
    
    [SerializeField,Range(0,10)] 
    private float healthRegenRate;
    
    [SerializeField, Range(0, 10)]
    private float healthDrainRate;

    #region Privates

    private float currentHealth;
    
    private Tween regenerateTween;
    private Tween drainTween;

    #endregion

    private void Awake()
    {
        instance = this;
        
        currentHealth = maxHealth;
    }

    public void Increase(int value)
    {
        currentHealth += value;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        PlayerHUD.Instance.UpdateHealthBar(currentHealth/maxHealth);
    }

    public void Decrease(int value)
    {
        currentHealth -= value;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            //Ölüm ve Yeniden Doğma
        }
        
        UpdateBar();
    }

    private void UpdateBar()
    {
        PlayerHUD.Instance.UpdateHealthBar(currentHealth/maxHealth);
    }

    public bool isRegenerateHealth
    {
        set
        {
            if (value)
            {
                if(regenerateTween != null && regenerateTween.IsPlaying()) return;
                
                regenerateTween = DOTween.To(() => currentHealth, p => currentHealth = p, maxHealth, currentHealth * healthRegenRate)
                    .OnUpdate(UpdateBar);
            }
            else
            {
                regenerateTween.Kill();
            }
        }
        
    }

    public bool isDrainHealth
    {
        set
        {
            if (value)
            {
                if(drainTween != null && drainTween.IsPlaying()) return;
                
                drainTween = DOTween.To(() => currentHealth, p => currentHealth = p, 0, currentHealth * healthDrainRate)
                    .OnUpdate(UpdateBar);
            }
            else
            {
                drainTween.Kill();
            }
        }
    }
    
}
