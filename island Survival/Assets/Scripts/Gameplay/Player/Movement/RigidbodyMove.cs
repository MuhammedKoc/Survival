using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class RigidbodyMove : MonoBehaviour
{
    [SerializeField] 
    private MovementValuesObject movementValues;
    public MovementValuesObject MovementValues => movementValues;
    
    [SerializeField]
    private PlayerStamina playerStamina;

    [SerializeField]
    private Rigidbody2D rb;

    #region Publics

    [HideInInspector]
    public Vector2 LastDirection => lastDirection;
    
    
    [HideInInspector]
    public Vector2 MoveDirection => moveDirection;

    public bool IsRunning;
    
    public bool IsRunable;

    #endregion
    
    #region Privates

    private float moveSpeed;
    
    private Vector2 lastDirection;
    private Vector2 moveDirection;
    
    #endregion
    
    private void Start()
    {
        moveSpeed = movementValues.Speed;
        IsRunable = true;
        
        InputManager.Controls.Player.Enable();
        
        InputManager.Controls.Player.Movement.performed += OnMovementPerformed;
        InputManager.Controls.Player.Movement.canceled += OnMovementCanceled;

        InputManager.Controls.Player.Run.performed += OnRunPerformed;
        InputManager.Controls.Player.Run.canceled += OnRunCanceled;
    }

    private void OnDestroy()
    {
        InputManager.Controls.Player.Disable();

        
        InputManager.Controls.Player.Movement.performed -= OnMovementPerformed;
        InputManager.Controls.Player.Movement.canceled -= OnMovementCanceled;

        InputManager.Controls.Player.Run.performed -= OnRunPerformed;
        InputManager.Controls.Player.Run.canceled -= OnRunCanceled;
    }

    public void SetVelocity(Vector2 value)
    {
        rb.velocity = value;
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
        IsRunning = true;
    }

    private void OnRunCanceled(InputAction.CallbackContext obj)
    {
        IsRunning = false;
    }
    #endregion
    
}
