using UnityEngine;
using UnityEngine.InputSystem;
using CodeCabana.StateMachine;
using CodeCabana.CharacterControllers;

namespace ArcticPass.CharacterControllers.Player
{
    [RequireComponent(typeof(Character))]
    public class IdleStatePlayer : State<Character>
    {
        public override void OnEnter(Character stateMachine)
        {
            Debug.Log("Entered idle state");
        }

        public override void OnExit(Character stateMachine)
        {
            Debug.Log("Exited idle state");
        }

        public override void OnUpdate(Character stateMachine)
        {
            Debug.Log("Updating idle State");
            stateMachine.ChangeState(GetComponent<MoveStatePlayer>());
        }
    }
}
