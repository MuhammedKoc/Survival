using Tmn.StateMachine;
using Unity.VisualScripting;

namespace Tmn.StateMachine
{
    public class StateBase
    {
        protected StateMachine stateMachine;

        protected StateBase(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public virtual void Enter()
        {
            
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}