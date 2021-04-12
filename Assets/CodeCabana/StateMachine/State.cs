using UnityEngine;

namespace CodeCabana.StateMachine
{
    public abstract class State<T> : MonoBehaviour
        where T : class
    {
        public abstract void OnEnter(T stateMachine);

        public abstract void OnExit(T stateMachine);

        public abstract void OnUpdate(T stateMachine);
    }
}
