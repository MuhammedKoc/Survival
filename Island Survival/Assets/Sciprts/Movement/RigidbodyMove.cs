using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMove : MonoBehaviour, IMovable
{
    [SerializeField] MovementValuesObject MovementValues;

    StaminaManager stamina;
    IControllable inputs;
    float MoveSpeed;
    Rigidbody2D rb;

    [SerializeField] bool isRunning;
    [SerializeField] public bool isRunable;

    private void Awake()
    {
        inputs = GetComponent<IControllable>();
        rb = GetComponent<Rigidbody2D>();
        stamina = GetComponent<StaminaManager>();
    }

    public void Move()
    {
        isRunning = inputs.GetRunBool();

        if (!isRunning)
        {
            stamina.AreSprinting = false;
        }

        if (isRunning && rb.velocity.magnitude > 0)
        {
            if(stamina.Stamina > 0)
            {
                //stamina.AreSprinting = true;
                stamina.StaminaRuninng();
            }
            else
            {
                isRunning = false;
            }
        }

        MoveSpeed = GetMoveSpeed();

        Vector2 move = inputs.GetMoveDirection().normalized * Time.fixedDeltaTime * MoveSpeed * 50;
        
        rb.velocity = move;
    }

    public float GetMoveSpeed()
    {
        if(!isRunable) return MovementValues.Speed;

        return inputs.GetRunBool() ? MovementValues.RunSpeed : MovementValues.Speed;
    }


    public void Space()
    {

    }
}
