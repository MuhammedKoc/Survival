using Tmn.Debugs;
using Tmn.States;
using UnityEngine;

namespace Tmn.StateMachine.States
{
    public class PlayerRunState : PlayerGroundState
    {
        public PlayerRunState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            moveSpeed = player.Move.MovementValues.RunSpeed;
            
            player.Stamina.StopRegenerate();
            player.Stamina.StartDrain();
            
            //Stamina drain start with dotween
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            //stamina kontrol
            //eğer biterse walk state'ine geç
            
            player.Animator.SetFloat("Horizontal", player.Move.MoveDirection.x);
            player.Animator.SetFloat("Vertical", player.Move.MoveDirection.y);
            
            if (!player.Move.IsRunning || !player.Move.IsRunable)
            {
                stateMachine.ChangeState(player.WalkState);
            }

            if (player.Move.MoveDirection == Vector2.zero)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            DebugHelper.LogPlayer("Exit Run");
            

            //stamina drain stop
            player.Stamina.StopDrain();
            
            Player.Instance.Move.SetVelocity(Vector2.zero);
        }
    }
}