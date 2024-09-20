using System;
using UnityEngine;

namespace Tmn.StateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        private StateMachine stateMachine;

        #region Singleton

        private static GameStateMachine instance = null;

        public static GameStateMachine Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log(instance.GetType().Name + "Instance is Null");
                }

                return instance;
            }
        }

        private void Awake()
        {
            instance = this;

            InitilaizeStates();
        }
        #endregion
        
        private void InitilaizeStates()
        {
            stateMachine = new StateMachine();
            
            
        }
    }
}