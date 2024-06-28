using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class RigidbodyMove : MonoBehaviour
{
    [SerializeField] MovementValuesObject movementValues;

    [SerializeField]
    private PlayerStamina playerStamina;

    [SerializeField]
    private Rigidbody2D rb;

    #region Publics

    [HideInInspector]
    public Vector2 LastDirection => lastDirection;
    
    
    [HideInInspector]
    public Vector2 MoveDirection => moveDirection;

    public bool IsRunable;

    #endregion
    
    #region Privates

    private float moveSpeed;
    
    private bool isRunning;
    
    private Vector2 lastDirection;
    private Vector2 moveDirection;
    
    #endregion
    
    private void Start()
    {
        moveSpeed = movementValues.Speed;
        IsRunable = true;
        
        InputManager.Instance.Controls.Player.Enable();
        
        InputManager.Instance.Controls.Player.Movement.performed += OnMovementPerformed;
        InputManager.Instance.Controls.Player.Movement.canceled += OnMovementCanceled;

        InputManager.Instance.Controls.Player.Run.performed += OnRunPerformed;
        InputManager.Instance.Controls.Player.Run.canceled += OnRunCanceled;
    }

    private void OnDestroy()
    {
        InputManager.Instance.Controls.Player.Disable();

        
        InputManager.Instance.Controls.Player.Movement.performed -= OnMovementPerformed;
        InputManager.Instance.Controls.Player.Movement.canceled -= OnMovementCanceled;

        InputManager.Instance.Controls.Player.Run.performed -= OnRunPerformed;
        InputManager.Instance.Controls.Player.Run.canceled -= OnRunCanceled;
    }

    public void Move()
    {
        if (!isRunning)
        {
            playerStamina.AreSprinting = false;
        }

        if (isRunning && rb.velocity.magnitude > 0)
        {
            if(playerStamina.Stamina > 0)
            {
                //stamina.AreSprinting = true;
                playerStamina.StaminaRuninng();
            }
            else
            {
                isRunning = false;
            }
        }

        moveSpeed = GetMoveSpeed();
        Vector2 move = moveDirection.normalized * Time.fixedDeltaTime * moveSpeed * 50;
        
        rb.velocity = move;
    }

    private float GetMoveSpeed()
    {
        if (!IsRunable) return movementValues.Speed;

        return (isRunning) ? movementValues.RunSpeed : movementValues.Speed;
    }
    
    #region Inputs

    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        moveDirection = obj.ReadValue<Vector2>();
    }
    
    private void OnMovementCanceled(InputAction.CallbackContext obj)
    {
        lastDirection = moveDirection;
        moveDirection = Vector2.zero;
    }

    
    private void OnRunPerformed(InputAction.CallbackContext obj)
    {
        isRunning = true;
        
        Debug.Log(IsRunable);
    }

    private void OnRunCanceled(InputAction.CallbackContext obj)
    {
        isRunning = false;
    }
    #endregion
    
}
