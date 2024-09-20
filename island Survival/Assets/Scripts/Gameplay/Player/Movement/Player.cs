using System;
using System.Collections;
using System.Collections.Generic;
using Tmn.StateMachine.States;
using Tmn.StateMachine;
using Tmn.States;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]
    private RigidbodyMove move;
    
    [SerializeField]
    private PlayerStamina stamina;

    [SerializeField]
    private Animator animator;
    
    public RigidbodyMove Move => move;
    public PlayerStamina Stamina => stamina;
    public Animator Animator => animator;
    
    //State Machine
    private StateMachine stateMachine;
    
    public StateMachine StateMachine => stateMachine;
    
    public PlayerIdleState IdleState;
    public PlayerWalkState WalkState;
    public PlayerRunState RunState;

    public PlayerUIState UIState;
    public PlayerActionState ActionState;
    
    #region Singleton

    private static Player instance;
    
    public static Player Instance
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

    private void Awake()
    {
        instance = this;
        
        IntializeStates();
    }
    
    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdate();   
    }

    private void IntializeStates()
    {
        stateMachine = new StateMachine();
        
        IdleState = new PlayerIdleState(this, stateMachine);
        WalkState = new PlayerWalkState(this, stateMachine);
        RunState = new PlayerRunState(this, stateMachine);
        UIState = new PlayerUIState(this, stateMachine);
        ActionState = new PlayerActionState(this, stateMachine);
        
        stateMachine.Initialize(IdleState);
    }
}
    