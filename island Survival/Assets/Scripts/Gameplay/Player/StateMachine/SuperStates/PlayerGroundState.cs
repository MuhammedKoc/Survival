using Tmn.StateMachine;
using UnityEngine;

namespace Tmn.States
{
    public class PlayerGroundState : PlayerStateBase
    {
        protected float moveSpeed;
        
        protected PlayerGroundState(Player player, StateMachine.StateMachine stateMachine) : base(player, stateMachine)
        {
            
        }
        
        public override void Enter()
        {
            base.Enter();
            
            InputManager.Instance.DisableAllMaps();
            InputManager.Controls.Player.Enable();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            Vector2 moveDirection = player.Move.MoveDirection;
            player.Move.SetVelocity(moveDirection.normalized * Time.fixedDeltaTime * moveSpeed * 50);
        }

        public override void Exit()
        {
            base.Exit();
            
            //inputs clear
            InputManager.Controls.Player.Disable();
        }
    }
}