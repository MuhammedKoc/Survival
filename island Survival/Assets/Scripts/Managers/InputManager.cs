using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Controls controls;
    [SerializeField] Vector2 moveDirection;
    bool isRunning;
    [SerializeField] Vector2 LastDirection;
    IMovable movable;

    [Serializable] public class InputManagerEvent : UnityEvent { }

    #region Singleton

    private static InputManager instance;
    
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log(instance.GetType().Name+"Instance is Null");
            }

            return instance;
        }

        private set {}
    }
    
    #endregion

    public Controls Controls => controls;
    
    private void Awake()
    {
        instance = this;
        
        controls = new Controls();
        movable = GetComponent<IMovable>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
