namespace CodeCabana.StateMachine
{
    public class StateMachine<T>
        where T : class
    {
        State<T> previousState;
        State<T> currentState;
        State<T> nextState;

        bool isStateChanging = true;

        public StateMachine(State<T> initState)
        {
            currentState = initState;
        }

        public void UpdateState(T state)
        {
            if(isStateChanging)
            {
                isStateChanging = false;
                currentState.OnExit(state);
                currentState = nextState;
                nextState.OnEnter(state);
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
            ChangeState(previousState);
        }
    }
}
