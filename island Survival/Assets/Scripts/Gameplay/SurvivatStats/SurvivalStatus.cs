using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Player;
using Tmn.Data;
using Tmn.Data.DataPersistence;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalStatus : MonoBehaviour, IDataPersistence
{
    #region Singelton

    private static SurvivalStatus instance;

    public static SurvivalStatus Instance
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
    public float CurrentHunger => currentHunger;

    private float currentThirst;
    public float CurrentThirst => currentThirst;

    
    #endregion

    private void Awake()
    {
        instance = this;
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
        UpdateThirstBar();
    }
    
    public void DecreaseThirst(float value)
    {
        currentThirst -= value;
        if (currentThirst <= 0)
        {
            currentThirst = 0;
        }

        CheckValuesForRegenOrDrainHealth();
        UpdateThirstBar();
    }

    #endregion

    public void CheckValuesForRegenOrDrainHealth()
    {
        if (currentHunger == 0 || currentThirst == 0)
        {
            PlayerHealth.Instance.ChangeDrain(true);

            PlayerHealth.Instance.ChangeRegenerate(false);
        }
        else if(currentHunger > maxHunger/2 && currentThirst > maxThirst/2)
        {
            PlayerHealth.Instance.ChangeDrain(false);

            PlayerHealth.Instance.ChangeRegenerate(true);
        }
        else if (currentHunger > 0 || currentThirst > 0)
        {
            PlayerHealth.Instance.ChangeDrain(false);

            PlayerHealth.Instance.ChangeRegenerate(false);

        }
    }

    private void UpdateHungerBar() => PlayerHUD.Instance.UpdateHungerBar(currentHunger/maxHunger);
    private void UpdateThirstBar() => PlayerHUD.Instance.UpdateThirstBar(currentThirst/maxThirst);

    
    public void LoadData(GameData data)
    {
        currentHunger = data.Hunger;
        currentThirst = data.Thirst;
        
        UpdateHungerBar();
        UpdateThirstBar();
    }
    
    public void SaveData(ref GameData data)
    {
        data.Hunger = currentHunger;
        data.Thirst = currentThirst;
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
