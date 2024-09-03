using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimation()
    {
        Vector2 direction = PlayerController.Instance.Move.MoveDirection;
        if (direction.x != 0 || direction.y != 0)
        {
            animator.SetBool("IsIdle", false);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        else if (direction.x == 0 && direction.y == 0)
        {
            Vector2 LastDir = PlayerController.Instance.Move.LastDirection;
            animator.SetBool("IsIdle", true);
            animator.SetFloat("LastHorizontal", LastDir.x);
            animator.SetFloat("LastVertical", LastDir.y);
        }
    }
}
