using Tmn.Debugs;
using Tmn.States;
using UnityEngine;

namespace Tmn.StateMachine.States
{
    public class PlayerWalkState : PlayerGroundState
    {
        public PlayerWalkState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            moveSpeed = player.Move.MovementValues.Speed;
            
            DebugHelper.LogPlayer("Enter Walk");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            player.Animator.SetFloat("Horizontal", player.Move.MoveDirection.x);
            player.Animator.SetFloat("Vertical", player.Move.MoveDirection.y);
            
            if (player.Move.MoveDirection == Vector2.zero)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if(player.Move.IsRunning && player.Move.IsRunable)
            {
                stateMachine.ChangeState(player.RunState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            Player.Instance.Move.SetVelocity(Vector2.zero);

        }
        
    }
}