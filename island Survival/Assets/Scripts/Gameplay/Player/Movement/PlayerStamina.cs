using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Main Parametres")]
    [SerializeField] MovementValuesObject movementValues;
    [SerializeField] public float Stamina=100;
    [SerializeField] private int MaxStamina=100;
    [SerializeField] public bool HasRegenerated = true;
    [SerializeField] public bool AreSprinting = false;

    [Header("Regenerate Parametres")]
    [Range(0, 50)][SerializeField] float StaminaDrain = 0.5f;
    [Range(0, 50)][SerializeField] float StaminaRegen = 0.5f;

    [SerializeField]
    private RigidbodyMove move;

    private void Awake()
    {
        Stamina = MaxStamina;
    }

    private void Update()
    {
        if (!AreSprinting)
        {
            if(Stamina <= MaxStamina - 0.01)
            {
                PlayerHUD.Instance.SetActiveStaminaFrame(true);
                Stamina += StaminaRegen * Time.deltaTime;
                UpdateStaminaBar();

                if(Stamina >= MaxStamina)
                {
                    move.IsRunable = true;
                    HasRegenerated = true;
                    PlayerHUD.Instance.SetActiveStaminaFrame(false);
                }
            }
        }
    }

    public void StaminaRuninng()
    {
        if(HasRegenerated)
        {
            PlayerHUD.Instance.SetActiveStaminaFrame(true);
            AreSprinting = true;
            Stamina-= StaminaDrain * Time.deltaTime;
            UpdateStaminaBar();

            if(Stamina <= 0)
            {
                move.IsRunable = false;
                Debug.Log(move.IsRunable);
                AreSprinting = false;
                HasRegenerated = false;
            }
        }
    }

    public void Reduce(float value)
    {
        if (Stamina - value < 0)
        {
            
        }
        else
        {
            Stamina -= value;
            UpdateStaminaBar();
            if(Stamina == 0)
            {
                //Stop the event
            }                           
        }
        
    }


    public void Increase(float value)
    {
        Stamina += value;
        if (Stamina > MaxStamina) Stamina = MaxStamina;
        UpdateStaminaBar();
    }


    private void UpdateStaminaBar()
    {
        PlayerHUD.Instance.UpdatesStaminaBar(Stamina/MaxStamina);
    }

    private IEnumerator RegenerateStamina()
    {
        
        yield return new WaitForSeconds(1f);
        Increase(1);

        if (Stamina == MaxStamina)
        {
            StopCoroutine(RegenerateStamina());
        }
        else
        {
            StartCoroutine(RegenerateStamina());
        }
    }
}

