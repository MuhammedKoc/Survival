using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class SurivalStatus : MonoBehaviour
{
    #region Singelton

    private static SurivalStatus instance;

    public static SurivalStatus Instance
    {
        get
        {
            if (instance == null)
            {
                
            }
            return instance;
        }
    }
    #endregion

    #region Values

    [Header("Values")]
    [SerializeField]
    private float maxHunger;
    
    [SerializeField]
    private float maxThirst;

    #endregion

    #region Privates

    private float currentHunger;

    private float currentThirst;

    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        
        PlayerHUD.Instance.UpdateHungerBar(currentHunger/maxHunger);
        PlayerHUD.Instance.UpdateThirstBar(currentThirst/maxThirst);
    }

    #region Hunger

    public void IncreaseHunger(float value)
    {
        currentHunger += value;
        if (currentHunger > maxHunger)
        {
            currentHunger = maxHunger;
        }
        
        CheckValuesForRegenOrDrainHealth();
        PlayerHUD.Instance.UpdateHungerBar(currentHunger/maxHunger);
    }
    
    public void DecreaseHunger(float value)
    {
        currentHunger -= value;
        if (currentHunger <= 0)
        {
            currentHunger = 0;
        }
        
        CheckValuesForRegenOrDrainHealth();
        PlayerHUD.Instance.UpdateHungerBar(currentHunger/maxHunger);
    }

    #endregion
    
    #region Hunger

    public void IncreaseThirst(float value)
    {
        currentThirst += value;
        if (currentThirst > maxThirst)
        {
            currentThirst = maxThirst;
        }

        CheckValuesForRegenOrDrainHealth();
        PlayerHUD.Instance.UpdateThirstBar(currentThirst/maxThirst);
    }
    
    public void DecreaseThirst(float value)
    {
        currentThirst -= value;
        if (currentThirst <= 0)
        {
            currentThirst = 0;
        }

        CheckValuesForRegenOrDrainHealth();
        PlayerHUD.Instance.UpdateHungerBar(currentThirst/maxThirst);
    }

    #endregion

    public void CheckValuesForRegenOrDrainHealth()
    {
        if (currentHunger == 0 || currentThirst == 0)
        {
            PlayerHealth.Instance.isDrainHealth = true;

            PlayerHealth.Instance.isRegenerateHealth = false;
        }
        else if(currentHunger > maxHunger/2 && currentThirst > maxThirst/2)
        {
            PlayerHealth.Instance.isDrainHealth = false;

            PlayerHealth.Instance.isRegenerateHealth = true;
        }
        else if (currentHunger > 0 || currentThirst > 0)
        {
            PlayerHealth.Instance.isDrainHealth = false;

            PlayerHealth.Instance.isRegenerateHealth = false;
        }
    }
}

#region Obsolote

// [SerializeField]
// public class SurvivalStat
// {
//     public int Statvalue { get; private set; }
//     public int StatMaxValue { get; private set; }
//     private Image bar;
//     public event Action<int> ChangeStat;
//
//
//     public SurvivalStat(int statMaxValue, Image bar)
//     {
//         StatMaxValue = statMaxValue;
//         this.bar = bar;
//
//         Statvalue = statMaxValue;
//     }
//
//     public void Increase(int value)
//     {
//         Statvalue += value;
//         if (Statvalue > StatMaxValue)
//         {
//             Statvalue = StatMaxValue;
//         }
//         ChangeStat?.Invoke(Statvalue);
//         UpdateUI();
//     }
//
//     public void Decrease(int value)
//     {
//         Statvalue -= value;
//         if (Statvalue <= 0)
//         {
//             Statvalue = 0;
//         }
//         ChangeStat?.Invoke(Statvalue);
//         UpdateUI();
//     }
//
//     public void UpdateUI()
//     {
//         bar.fillAmount = (float)Statvalue / StatMaxValue;
//     }
// }

#endregion
