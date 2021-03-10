using UnityEngine;
using CodeCabana.StateMachine;

namespace ArcticPass.Character
{
    public class MoveStatePlayer : State
    {
        PlayerController controller;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
        }

        public override void OnEnter(StateMachine stateMachine)
        {
            
        }

        public override void OnExit(StateMachine stateMachine)
        {
            
        }

        public override void OnUpdate(StateMachine stateMachine)
        {
            
        }
    }
}
