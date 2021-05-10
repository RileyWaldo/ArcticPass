using UnityEngine;

namespace ArcticPass.CharacterControllers.Combat
{
    public class Combat : MonoBehaviour, ICombat
    {
        bool isAttacking = false;

        public void Attack()
        {
            isAttacking = true;
        }

        public void EndAttack()
        {
            isAttacking = false;
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }
    }
}
