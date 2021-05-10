namespace ArcticPass.CharacterControllers.Combat
{
    public interface ICombat
    {
        bool IsAttacking();
        void Attack();
        void EndAttack();
    }
}
