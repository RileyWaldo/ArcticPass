namespace ArcticPass.CharacterControllers
{
    public interface IState
    {
        void EnterState(StateHandler controller);
        void ExitState(StateHandler controller);
        void UpdateState(StateHandler controller);
        CharacterState GetStateType();
    }
}
