using UnityEngine;
using UnityEngine.Assertions;
using CodeCabana.Core;
using ArcticPass.CharacterControllers.Movement;
using ArcticPass.CharacterControllers.Combat;

namespace ArcticPass.CharacterControllers
{
    public class Character : MonoBehaviour
    {
        public Health Health { private set; get; }
        public IMovement Movement { private set; get; }
        public ICombat Attack { private set; get; }
        public Animator Animator { private set; get; }

        private void Awake()
        {
            Health = GetComponent<Health>();
            Movement = GetComponent<IMovement>();
            Attack = GetComponent<ICombat>();
            Animator = GetComponent<Animator>();

            AssertSetUp();
        }

        private void Death()
        {

        }

        private void OnEnable()
        {
            Health.OnDeath += Death;
        }

        private void OnDisable()
        {
            Health.OnDeath -= Death;
        }

        private void AssertSetUp()
        {
            Assert.IsNotNull(Health, "Character requires Health component.");
            Assert.IsNotNull(Movement, "Character requires IMovement component.");
            Assert.IsNotNull(Attack, "Character requires ICombat component.");
            Assert.IsNotNull(Animator, "Character requires Animator component.");
        }
    }
}
