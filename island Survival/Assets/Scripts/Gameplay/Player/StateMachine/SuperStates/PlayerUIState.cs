using Tmn.StateMachine;

namespace Tmn.States
{
    public class PlayerUIState : PlayerStateBase
    {
        public PlayerUIState(Player player, StateMachine.StateMachine stateMachine) : base(player, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            
            InputManager.Instance.DisableAllMaps();
            InputManager.Controls.UI.Enable();
        }
    }
}