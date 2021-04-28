namespace CodeCabana.StateMachine
{
    public class StateMachine<T>
        where T : class
    {
        State<T> previousState;
        State<T> currentState;
        State<T> nextState;

        bool isStateChanging = false;

        public StateMachine(State<T> initState)
        {
            ChangeState(initState);
        }

        public void UpdateState(T state)
        {
            if(isStateChanging)
            {
                isStateChanging = false;

                if(currentState != null)
                    currentState.OnExit(state);

                currentState = nextState;
                currentState.OnEnter(state);
            }
            currentState.OnUpdate(state);
        }

        public void ChangeState(State<T> newState)
        {
            isStateChanging = true;
            previousState = currentState;
            nextState = newState;
        }

        public void RestorePreviousState()
        {
            if (previousState == null)
                return;

            ChangeState(previousState);
        }
    }
}
