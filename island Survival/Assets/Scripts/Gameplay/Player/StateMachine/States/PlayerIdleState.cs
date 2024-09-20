using Tmn.Debugs;
using Tmn.States;
using UnityEngine;

namespace Tmn.StateMachine.States
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            //animation
            // DebugHelper.LogPlayer("Enter Indle");
            
            player.Animator.SetBool("IsIdle", true);
            player.Animator.SetFloat("LastHorizontal", player.Move.LastDirection.x);
            player.Animator.SetFloat("LastVertical", player.Move.LastDirection.y);
        }

        public override void LogicUpdate()
        {
            if (player.Move.MoveDirection != Vector2.zero)
            {
                if (player.Move.IsRunning && player.Move.IsRunable)
                {
                    stateMachine.ChangeState(player.RunState);
                    DebugHelper.LogSystem("Enter Run");
                }
                else
                {
                    stateMachine.ChangeState(player.WalkState);                
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            player.Animator.SetBool("IsIdle", false);
        }
    }
}