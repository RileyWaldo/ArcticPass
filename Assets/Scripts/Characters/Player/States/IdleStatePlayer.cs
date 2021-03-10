using UnityEngine;
using UnityEngine.InputSystem;
using CodeCabana.StateMachine;

namespace ArcticPass.Character
{
    public class IdleStatePlayer : State, IPlayerState
    {
        public override void OnEnter(StateMachine stateMachine)
        {
            Debug.Log("Entered state");
        }

        public override void OnExit(StateMachine stateMachine)
        {
            Debug.Log("Exited state");
        }

        public override void OnUpdate(StateMachine stateMachine)
        {
            Debug.Log("Updating State");
        }

        public void OnAttack()
        {
            Debug.Log("Player Idle OnAttack!");
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            
        }
    }
}
