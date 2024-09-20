namespace Tmn.StateMachine
{
    public class PlayerStateBase : StateBase
    {
        protected Player player;

        protected PlayerStateBase(Player player, StateMachine stateMachine) : base(stateMachine)
        {
            this.player = player;
        }
    }
}