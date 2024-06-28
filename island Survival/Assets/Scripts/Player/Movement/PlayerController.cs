using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    bool isRunning;

    [SerializeField]
    private RigidbodyMove move;
    public RigidbodyMove Move => move;

    [SerializeField]
    private PlayerAnimation animation;
    
    #region Singleton

    private static PlayerController instance;
    
    public static PlayerController Instance
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
    }

    void Start()
    {
        move = GetComponent<RigidbodyMove>();
    }

    void Update()
    {
        animation.UpdateAnimation();
    }

    private void FixedUpdate()
    {
        move.Move();
    }
}
    