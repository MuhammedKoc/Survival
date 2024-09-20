using Tmn.StateMachine;
using Tmn.StateMachine.States;

namespace Tmn.States
{
    public class PlayerActionState : PlayerStateBase
    {
        public PlayerActionState(Player player, StateMachine.StateMachine stateMachine) : base(player, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            
            InputManager.Instance.DisableAllMaps();
        }
    }
}