using Tmn.StateMachine;
using UnityEngine;

namespace Tmn.StateMachine
{
    public class StateMachine
    {
        public StateBase CurrentState { get; private set; }
        
        public void Initialize(StateBase startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(StateBase newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}