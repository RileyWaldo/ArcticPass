using UnityEngine;
using UnityEngine.Assertions;

namespace CodeCabana.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        [SerializeField] State startingState = null;

        State currentState;
        State nextState;

        bool isStateChanging = false;

        protected void AwakeState()
        {
            Assert.IsNotNull(startingState, "Please set initial state for " + name);
        }

        protected void StartState()
        {
            InitStartState();
        }

        protected void UpdateState()
        {
            currentState.OnUpdate(this);
            if(isStateChanging)
            {
                isStateChanging = false;
                currentState.OnExit(this);
                currentState = nextState;
                nextState.OnEnter(this);
            }
        }

        protected void InitStartState()
        {
            currentState = startingState;
            currentState.OnEnter(this);
        }

        public void ChangeState(State newState)
        {
            isStateChanging = true;
            nextState = newState;
        }
    }
}
