using UnityEngine;
using CodeCabana.StateMachine;
using CodeCabana.CharacterControllers;

namespace ArcticPass.CharacterControllers.Player
{
    [RequireComponent(typeof(Character))]
    public class MoveStatePlayer : State<Character>
    {
        public override void OnEnter(Character stateMachine)
        {
            Debug.Log("Entered move State");
        }

        public override void OnExit(Character stateMachine)
        {
            Debug.Log("Exited move State");
        }

        public override void OnUpdate(Character stateMachine)
        {
            Debug.Log("Updating move State");
            stateMachine.ChangeState(GetComponent<IdleStatePlayer>());
        }
    }
}
