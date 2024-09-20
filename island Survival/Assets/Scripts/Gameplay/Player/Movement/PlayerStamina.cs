using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tmn;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Main Parametres")]
    [SerializeField] MovementValuesObject movementValues;
    [SerializeField] public float CurrentStamina=100;
    [SerializeField] private int maxStamina=100;
    [SerializeField] public bool HasRegenerated = true;
    [SerializeField] public bool AreSprinting = false;

    [Header("Regenerate Parametres")]
    [Range(0, 50)][SerializeField] float StaminaDrainRate = 0.5f;
    [Range(0, 50)][SerializeField] float StaminaRegenRate = 0.5f;

    [SerializeField]
    private RigidbodyMove move;

    private Tween drainTween;
    private Tween regenerateTween;

    private void Awake()
    {
        CurrentStamina = maxStamina;
    }

    public void StartDrain()
    {
        if (CurrentStamina > 0)
        {
            PlayerHUD.Instance.SetActiveStaminaFrame(true);
            drainTween = DOTween.To(() => CurrentStamina, p => CurrentStamina = p, 0, CurrentStamina * StaminaDrainRate).OnUpdate((
                () =>
                {
                    UpdateStaminaBar();
                })).OnComplete((
                () =>
                {
                    move.IsRunable = false;
                    move.IsRunning = false;
                    drainTween.Kill();
                    
                    StartRegenerate();
                }));
        }
    }

    public void StopDrain()
    {
        drainTween.Kill();
        StartRegenerate();
    }
    
    public void StartRegenerate()
    {
        if (CurrentStamina != maxStamina - 0.01)
        {
            regenerateTween = DOTween.To(() => CurrentStamina, p => CurrentStamina = p, maxStamina, (maxStamina-CurrentStamina) * StaminaRegenRate).OnUpdate((
                () =>
                {
                    UpdateStaminaBar();
                })).OnComplete((
                () =>
                {
                    move.IsRunable = true;
                    PlayerHUD.Instance.SetActiveStaminaFrame(false);
                }));
        }
    }

    public void StopRegenerate()
    {
        regenerateTween.Kill();
    }

    // private void Update()
    // {
    //     if (!AreSprinting)
    //     {
    //         if(CurrentStamina <= maxStamina - 0.01)
    //         {
    //             PlayerHUD.Instance.SetActiveStaminaFrame(true);
    //             CurrentStamina += StaminaRegen * Time.deltaTime;
    //             UpdateStaminaBar();
    //
    //             if(CurrentStamina >= maxStamina)
    //             {
    //                 move.IsRunable = true;
    //                 HasRegenerated = true;
    //                 PlayerHUD.Instance.SetActiveStaminaFrame(false);
    //             }
    //         }
    //     }
    // }
    //
    // public void StaminaRuninng()
    // {
    //     if(HasRegenerated)
    //     {
    //         PlayerHUD.Instance.SetActiveStaminaFrame(true);
    //         AreSprinting = true;
    //         CurrentStamina-= StaminaDrain * Time.deltaTime;
    //         UpdateStaminaBar();
    //
    //         if(CurrentStamina <= 0)
    //         {
    //             move.IsRunable = false;
    //             Debug.Log(move.IsRunable);
    //             AreSprinting = false;
    //             HasRegenerated = false;
    //         }
    //     }
    // }

    public void Reduce(float value)
    {
        if (CurrentStamina - value < 0)
        {
            
        }
        else
        {
            CurrentStamina -= value;
            UpdateStaminaBar();
            if(CurrentStamina == 0)
            {
                //Stop the event
            }                           
        }
        
    }


    public void Increase(float value)
    {
        CurrentStamina += value;
        if (CurrentStamina > maxStamina) CurrentStamina = maxStamina;
        UpdateStaminaBar();
    }


    private void UpdateStaminaBar()
    {
        PlayerHUD.Instance.UpdatesStaminaBar(CurrentStamina/maxStamina);
    }

    private IEnumerator RegenerateStamina()
    {
        
        yield return new WaitForSeconds(1f);
        Increase(1);

        if (CurrentStamina == maxStamina)
        {
            StopCoroutine(RegenerateStamina());
        }
        else
        {
            StartCoroutine(RegenerateStamina());
        }
    }
}

