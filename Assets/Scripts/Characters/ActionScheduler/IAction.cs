namespace ArcticPass.CharacterControllers.Actions
{
    public interface IAction
    {
        bool CanOverride();
        void Cancel();
    }
}
