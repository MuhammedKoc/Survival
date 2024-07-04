using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SurivalStatus : MonoBehaviour
{
    #region Instance

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


    [Header("Hunger")]
    public SurvivalStat Hungry;
    [SerializeField] Image HungerBar;

    [Header("Thirst")]
    public SurvivalStat Thirst;
    [SerializeField] Image ThirstBar;

    private void Awake()
    {
        instance = this;

        Hungry = new SurvivalStat(50, HungerBar);
        Thirst = new SurvivalStat(50, ThirstBar);
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        Hungry.ChangeStat += OnChangeStat;
        Thirst.ChangeStat += OnChangeStat;

    }

    private void OnDisable()
    {
        Hungry.ChangeStat -= OnChangeStat;
        Thirst.ChangeStat -= OnChangeStat;
    }

    public void OnChangeStat(int survivalStatValue)
    {
        switch (survivalStatValue)
        {
            case 0:
                PlayerHealth.Instance.isDegenHealth = true;

                PlayerHealth.Instance.isRegenHealth = false;
                break;

            case 50:
                PlayerHealth.Instance.isDegenHealth = false;

                PlayerHealth.Instance.isRegenHealth = true;
                break;

            case > 0:
                PlayerHealth.Instance.isDegenHealth = false;

                PlayerHealth.Instance.isRegenHealth = false;
                break;
        }
    }
}

[SerializeField]
public class SurvivalStat
{
    public int Statvalue { get; private set; }
    public int StatMaxValue { get; private set; }
    private Image bar;
    public event Action<int> ChangeStat;


    public SurvivalStat(int statMaxValue, Image bar)
    {
        StatMaxValue = statMaxValue;
        this.bar = bar;

        Statvalue = statMaxValue;
    }

    public void Increase(int value)
    {
        Statvalue += value;
        if (Statvalue > StatMaxValue)
        {
            Statvalue = StatMaxValue;
        }
        ChangeStat?.Invoke(Statvalue);
        UpdateUI();
    }

    public void Decrease(int value)
    {
        Statvalue -= value;
        if (Statvalue <= 0)
        {
            Statvalue = 0;
        }
        ChangeStat?.Invoke(Statvalue);
        UpdateUI();
    }

    public void UpdateUI()
    {
        bar.fillAmount = (float)Statvalue / StatMaxValue;
    }
}
