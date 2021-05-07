using UnityEngine;
using ArcticPass.CharacterControllers.Actions;

namespace ArcticPass.CharacterControllers.Combat
{
    public class Combat : MonoBehaviour, ICombat
    {
        Character character;

        bool isAttacking = false;

        private void Awake()
        {
            character = GetComponent<Character>();
        }

        private void Update()
        {
            if (!isAttacking)
                return;

            if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
        }

        public void Attack()
        {
            isAttacking = true;
        }

        public void Cancel()
        {
            isAttacking = false;
            character.Animator.SetBool("attack", false);
        }
    }
}
