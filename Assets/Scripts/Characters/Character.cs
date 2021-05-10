using UnityEngine;
using UnityEngine.Assertions;
using CodeCabana.Core;
using ArcticPass.CharacterControllers.Movement;
using ArcticPass.CharacterControllers.Combat;
using ArcticPass.CharacterControllers.Animations;

namespace ArcticPass.CharacterControllers
{
    public class Character : MonoBehaviour
    {
        Health health;
        IMovement movement;
        ICombat attack;
        Animator animator;

        static readonly int hashHorizontal = Animator.StringToHash("Horizontal");
        static readonly int hashVertical = Animator.StringToHash("Vertical");
        static readonly int hashAttack = Animator.StringToHash("Attack");

        private void Awake()
        {
            health = GetComponent<Health>();
            movement = GetComponent<IMovement>();
            attack = GetComponent<ICombat>();
            animator = GetComponent<Animator>();

            AssertSetUp();

            CharacterAnimationState.Initialize(animator.GetBehaviour<CharacterAnimationState>(), this);
        }

        private void Death()
        {
            
        }

        private void OnEnable()
        {
            health.OnDeath += Death;
        }

        private void OnDisable()
        {
            health.OnDeath -= Death;
        }

        private void AssertSetUp()
        {
            Assert.IsNotNull(health, "Character requires Health component.");
            Assert.IsNotNull(movement, "Character requires IMovement component.");
            Assert.IsNotNull(attack, "Character requires ICombat component.");
            Assert.IsNotNull(animator, "Character requires Animator component.");
        }

        public void Move(Vector2 direction)
        {
            if (attack.IsAttacking())
                return;

            animator.speed = 1f;
            animator.SetFloat(hashHorizontal, direction.x);
            animator.SetFloat(hashVertical, direction.y);
            movement.SetVelocity(direction * 5f);
        }

        public void MoveEnd()
        {
            if (!attack.IsAttacking())
                animator.speed = 0;
            movement.SetVelocity(Vector2.zero);
        }

        public void Attack()
        {
            if (attack.IsAttacking())
                return;

            animator.speed = 1f;
            animator.SetTrigger(hashAttack);
            attack.Attack();
        }

        public void OnAnimationEnd(AnimatorStateInfo stateInfo)
        {
            if(stateInfo.shortNameHash == hashAttack)
            {
                attack.EndAttack();
            }
        }
    }
}
