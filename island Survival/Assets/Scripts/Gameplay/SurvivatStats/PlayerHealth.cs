using System.Collections;
using DG.Tweening;
using Player;
using Tmn.Data;
using Tmn.Data.DataPersistence;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDataPersistence
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

    [SerializeField]
    private float regenerateDrainStopTime;

    #region Privates

    private float currentHealth;
    public float CurrentHealth => currentHealth;

    private bool isRegenerate;
    private bool isDrain;
    
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
        StopRegenerateOrDrain();
        
        currentHealth += value;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateBar();
    }

    public void Decrease(int value)
    {
        StopRegenerateOrDrain();
        
        currentHealth -= value;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            //Ölüm ve Yeniden Doğma
        }
        
        UpdateBar();
    }

    private void UpdateBar() => PlayerHUD.Instance.UpdateHealthBar(currentHealth/maxHealth);
    

    public void ChangeRegenerate(bool value)
    {
        isRegenerate = value;
        
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

    public void ChangeDrain(bool value)
    {
        isDrain = value;
        
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
    
    private void StopRegenerateOrDrain()
    {
        if (isRegenerate)
        {
            ChangeRegenerate(false);
            
            DOVirtual.DelayedCall(regenerateDrainStopTime, () =>
            {
                ChangeRegenerate(true);
                SurvivalStatus.Instance.CheckValuesForRegenOrDrainHealth();

            });
        }
        
        if (isDrain)
        {
            ChangeDrain(false);
            
            DOVirtual.DelayedCall(regenerateDrainStopTime, () =>
            {
                ChangeDrain(true);
                SurvivalStatus.Instance.CheckValuesForRegenOrDrainHealth();
            });
        }
    }

    public void LoadData(GameData data)
    {
        currentHealth = data.Health;
        
        UpdateBar();
    }

    public void SaveData(ref GameData data)
    {
        data.Health = currentHealth;
    }
}
