using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IControllable
{

    Controls controls;
    [SerializeField] Vector2 moveDirection;
    bool isRunning;
    [SerializeField] Vector2 LastDirection;
    IMovable movable;

    private void Awake()
    {
        controls = new Controls();
        movable = GetComponent<IMovable>();
    }

    private void Update()
    {
        if (controls.Player.Dash.WasPressedThisFrame())
        {

        }
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.Movement.performed += OnMovementPerformed;
        controls.Player.Movement.canceled += OnMovementCancelled;

        controls.Player.Run.performed += OnRunPerformed;
        controls.Player.Run.canceled += OnRunCancelled;
    }

    private void OnDisable()
    {
        controls.Disable();

        controls.Player.Movement.performed -= OnMovementPerformed;
        controls.Player.Movement.canceled -= OnMovementCancelled;

        controls.Player.Run.performed -= OnRunPerformed;
        controls.Player.Run.canceled -= OnRunCancelled;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public Vector2 GetLastDirection()
    {
        return LastDirection;
    }
    public bool GetRunBool()
    {
        return isRunning;
    }

    #region Performed

    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        moveDirection = obj.ReadValue<Vector2>();
    }

    private void OnRunPerformed(InputAction.CallbackContext obj)
    {
        isRunning = obj.action.WasPressedThisFrame();
    }
    #endregion

    #region Cancelled

    private void OnMovementCancelled(InputAction.CallbackContext obj)
    {
        LastDirection = moveDirection;
        moveDirection = Vector2.zero;
    }

    private void OnRunCancelled(InputAction.CallbackContext obj)
    {
        isRunning = false;
    }
    #endregion
}
