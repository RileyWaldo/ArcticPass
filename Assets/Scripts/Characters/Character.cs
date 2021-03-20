using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using CodeCabana.StateMachine;
using CodeCabana.Core;

namespace ArcticPass.Character
{
    public class Character : StateMachine<Character>
    {
        [SerializeField] State<Character> idleState = null;
        [SerializeField] State<Character> moveState = null;
        [SerializeField] State<Character> attackState = null;
        [SerializeField] State<Character> deadState = null;

        List<CharacterAbility> abilities = new List<CharacterAbility>();

        public Health Health { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }
        public Animator Animator { get; private set; }

        public State<Character> IdleState { get => idleState; }
        public State<Character> MoveState { get => moveState; }
        public State<Character> AttackState { get => attackState; }
        public State<Character> DeadState { get => deadState; }

        private void Awake()
        {
            Health = GetComponent<Health>();
            RigidBody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();

            AssertValuesAreSet();

            AwakeState();
        }

        private void Start()
        {
            StartState(this);
        }

        private void Update()
        {
            UpdateState(this);
            UpdateAbilities();
        }

        private void UpdateAbilities()
        {
            foreach (CharacterAbility ability in abilities)
            {
                if(ability.IsActive)
                    ability.OnUpdateAbility();
            }
        }

        private void OnDeath()
        {
            ChangeState(deadState);
        }

        private void OnEnable()
        {
            Health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            Health.OnDeath -= OnDeath;
        }

        private void AssertValuesAreSet()
        {
            Assert.IsNotNull(Health, name + " is missing health component.");
            Assert.IsNotNull(RigidBody, name + " is missing rigidbody2D component.");
            Assert.IsNotNull(Animator, name + " is missing animator component.");
            Assert.IsNotNull(idleState, name + " missing idle state.");
            Assert.IsNotNull(moveState, name + " missing move state.");
            Assert.IsNotNull(attackState, name + " missing attack state.");
            Assert.IsNotNull(deadState, name + " missing dead state.");
        }

        public void SetIdleState(State<Character> state)
        {
            idleState = state;
        }

        public void SetMoveState(State<Character> state)
        {
            moveState = state;
        }

        public void SetAttackState(State<Character> state)
        {
            attackState = state;
        }

        public void SetDeadState(State<Character> state)
        {
            deadState = state;
        }

        public void AddAbility(CharacterAbility ability)
        {
            abilities.Add(ability);
        }

        public void RemoveAbility(CharacterAbility ability)
        {
            abilities.Remove(ability);
        }
    }
}