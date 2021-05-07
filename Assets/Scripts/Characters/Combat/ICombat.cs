using ArcticPass.CharacterControllers.Actions;

namespace ArcticPass.CharacterControllers.Combat
{
    public interface ICombat : IAction
    {
        void Attack();
    }
}
