using UnityEngine;

namespace ArcticPass.CharacterControllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class StateHandler : MonoBehaviour
    {
        [SerializeField] CharacterState startingState = CharacterState.Idle;

        IState[] states;
        IState currentState;
        IState nextState;

        bool isStateChanging = false;

        void Awake()
        {
            states = GetComponents<IState>();
        }

        void Start()
        {
            InitStartState();
        }

        void Update()
        {
            ProcessState();
        }

        private void ProcessState()
        {
            currentState.UpdateState(this);
            if (isStateChanging)
            {
                isStateChanging = false;
                currentState.ExitState(this);
                currentState = nextState;
                nextState.EnterState(this);
            }
        }

        private void InitStartState()
        {
            currentState = GetState(startingState);
            if (currentState != null)
            {
                currentState.EnterState(this);
            }
        }

        public IState GetState(CharacterState stateToCheck)
        {
            foreach (IState state in states)
            {
                if (stateToCheck == state.GetStateType())
                {
                    return state;
                }
            }

            Debug.LogWarning(this + " Does not contain state: " + stateToCheck);
            return null;
        }

        public void TransitionState(CharacterState state)
        {
            nextState = GetState(state);
            isStateChanging = true;
        }
    }
}
