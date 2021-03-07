using UnityEngine;
using CodeCabana.StateMachine;

public class IdleStatePlayer : State
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
}
