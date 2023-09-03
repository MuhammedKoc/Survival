using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isRunning;

    IMovable Physic;
    IAnimateable Animate;

    void Start()
    {
        Physic = GetComponent<IMovable>();
        Animate = GetComponent<IAnimateable>(); 
    }

    void Update()
    {
       Animate.UpdateAnimation();
    }

    private void FixedUpdate()
    {
        Physic.Move();
    }
}
