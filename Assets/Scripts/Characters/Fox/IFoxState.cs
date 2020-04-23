namespace ArcticPass.AI
{
    public interface IFoxState
    {
        void OnStateEnter(AIFox fox);
        void OnStateExit(AIFox fox);
        void OnStateUpdate(AIFox fox);
    }
}
