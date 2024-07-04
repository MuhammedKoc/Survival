using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerHealth : MonoBehaviour, IStatable
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

    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth = 100;

    [SerializeField] private GameObject Bar;

    [SerializeField,Range(0,10)] float healthRegenRate;
    [SerializeField, Range(0, 10)] float healtheDegenRate;

    private bool RegenBool;
    private bool DegenBool;

    private void Awake()
    {
        instance = this;
        Health = MaxHealth;
        
        Decrease(25);
    }

    public void Increase(int value)
    {
        Health += value;
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        UpdateUI();
    }

    public void Decrease(int value)
    {
        Health -= value;
        if(Health <= 0)
        {
            Health = 0;
            //Ölüm ve Yeniden Doğma
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        Bar.GetComponent<Image>().fillAmount = (float)Health/MaxHealth;
    }

    public bool isRegenHealth
    {
        set
        {
            if (value)
            {
                RegenBool = true;
                StartCoroutine(RegenerateHealth());
            }
            else if (!value)
            {
                RegenBool = false;
            }
        }
        
    }

    public IEnumerator RegenerateHealth()
    {
        while (Health < MaxHealth && RegenBool)
        {
            Increase(1);
            yield return new WaitForSeconds(healthRegenRate);
        }
    }

    public bool isDegenHealth
    {
        set
        {
            if(value)
            {
                DegenBool = true;
                StartCoroutine(DegenerateHealth());
            }
            else if(!value)
            {
                DegenBool = false;
            }
        }
    }

    public IEnumerator DegenerateHealth()
    {
        while (Health > 0 && DegenBool)
        {
            Decrease(1);
            yield return new WaitForSeconds(healthRegenRate);
        }
    }
}
