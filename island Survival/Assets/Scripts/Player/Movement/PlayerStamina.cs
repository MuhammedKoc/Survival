using System.Collections;
using System.Collections.Generic;
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



    [Header("Ui")]
    [SerializeField] private Image Bar;

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
                Bar.transform.parent.gameObject.SetActive(true);
                Stamina += StaminaRegen * Time.deltaTime;
                UpdateStaminaBar();

                if(Stamina >= MaxStamina)
                {
                    move.IsRunable = true;
                    HasRegenerated = true;
                    Bar.transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }

    public void StaminaRuninng()
    {
        if(HasRegenerated)
        {
            Bar.transform.parent.gameObject.SetActive(true);
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
        Bar.fillAmount = Stamina/MaxStamina;
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

