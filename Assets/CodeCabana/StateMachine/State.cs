using UnityEngine;

namespace CodeCabana.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public abstract void OnEnter(StateMachine stateMachine);

        public abstract void OnExit(StateMachine stateMachine);

        public abstract void OnUpdate(StateMachine stateMachine);
    }
}
