using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IControllable
{
    Controls controls;
    [SerializeField] Vector2 moveDirection;
    bool isRunning;
    [SerializeField] Vector2 LastDirection;
    IMovable movable;

    [Serializable] public class InputManagerEvent : UnityEvent { }

    [SerializeField] InputManagerEvent OpenInventory;

    [SerializeField] InputManagerEvent OnLeftClick;

    [SerializeField] InputManagerEvent OnRightClick;

    public event Action<int> SlotChange;

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

        controls.UI.Inventory.performed += OnInventoryPerformed;

        controls.UI.MouseLeftClick.performed += OnLeftClickPerformed;

        controls.UI.MouseRightClick.performed += OnRightClickPerformed;

        controls.UI.Slotbar.performed += ctx => SlotChange.Invoke(int.Parse(ctx.control.name));
    }

    private void OnDisable()
    {
        controls.Disable();

        controls.Player.Movement.performed -= OnMovementPerformed;
        controls.Player.Movement.canceled -= OnMovementCancelled;

        controls.Player.Run.performed -= OnRunPerformed;
        controls.Player.Run.canceled -= OnRunCancelled;

        controls.UI.Inventory.performed -= OnInventoryPerformed;
        controls.UI.MouseLeftClick.performed -= OnLeftClickPerformed;

        controls.UI.MouseRightClick.performed -= OnRightClickPerformed;
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

    private void OnInventoryPerformed(InputAction.CallbackContext obj)
    {
        OpenInventory.Invoke();       
    }

    private void OnLeftClickPerformed(InputAction.CallbackContext obj)
    {
        if (!ArteusLibrary.IsPointerOverUIElement())
        {
            OnLeftClick.Invoke();
        }
    }

    private void OnRightClickPerformed(InputAction.CallbackContext obj)
    {
        OnRightClick.Invoke();
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
