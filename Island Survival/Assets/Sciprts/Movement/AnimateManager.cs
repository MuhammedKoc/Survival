using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateManager : MonoBehaviour, IAnimateable
{
    Animator animator;
    IControllable inputs;
    IMovable movable;
    private void Start()
    {
        animator = GetComponent<Animator>();
        inputs = GetComponent<IControllable>();
        movable = GetComponent<IMovable>();
    }

    public void UpdateAnimation()
    {
        Vector2 direction = inputs.GetMoveDirection();
        if (direction.x != 0 || direction.y != 0)
        {
            animator.SetBool("IsIdle", false);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        else if (direction.x == 0 && direction.y == 0)
        {
            Vector2 LastDir = inputs.GetLastDirection();
            animator.SetBool("IsIdle", true);
            animator.SetFloat("LastHorizontal", LastDir.x);
            animator.SetFloat("LastVertical", LastDir.y);
        }


    }



}
