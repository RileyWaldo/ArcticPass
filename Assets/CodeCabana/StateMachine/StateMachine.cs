using UnityEngine;
using UnityEngine.Assertions;

namespace CodeCabana.StateMachine
{
    public abstract class StateMachine<T> : MonoBehaviour
        where T : class
    {
        [SerializeField] State<T> startingState = null;

        State<T> previousState;
        State<T> currentState;
        State<T> nextState;

        bool isStateChanging = false;

        protected void AwakeState()
        {
            Assert.IsNotNull(startingState, "Please set initial state for " + name);
        }

        protected void StartState(T state)
        {
            InitStartState(state);
        }

        protected void UpdateState(T state)
        {
            currentState.OnUpdate(state);
            if(isStateChanging)
            {
                isStateChanging = false;
                currentState.OnExit(state);
                currentState = nextState;
                nextState.OnEnter(state);
            }
        }

        protected void InitStartState(T state)
        {
            currentState = startingState;
            currentState.OnEnter(state);
        }

        public void ChangeState(State<T> newState)
        {
            isStateChanging = true;
            previousState = currentState;
            nextState = newState;
        }

        public void RestorePreviousState()
        {
            ChangeState(previousState);
        }
    }
}
