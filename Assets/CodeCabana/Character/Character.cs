using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using CodeCabana.StateMachine;
using CodeCabana.Core;

namespace CodeCabana.CharacterControllers
{
    public class Character : MonoBehaviour
    {
        [SerializeField] State<Character> startingState = null;

        StateMachine<Character> stateMachine;
        List<CharacterAbility> abilities = new List<CharacterAbility>();

        public Health Health { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }
        public Animator Animator { get; private set; }

        private void Awake()
        {
            Health = GetComponent<Health>();
            RigidBody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();

            AssertValuesAreSet();

            stateMachine = new StateMachine<Character>(startingState);
        }

        private void Start()
        {
            GetAbilities();
        }

        private void Update()
        {
            stateMachine.UpdateState(this);
            UpdateAbilities();
        }

        private void GetAbilities()
        {
            CharacterAbility[] characterAbilities = GetComponents<CharacterAbility>();
            foreach (CharacterAbility ability in characterAbilities)
            {
                AddAbility(ability);
            }
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
            Assert.IsNotNull(startingState, name + "is missing starting state.");
            Assert.IsNotNull(Health, name + " is missing health component.");
            Assert.IsNotNull(RigidBody, name + " is missing rigidbody2D component.");
            Assert.IsNotNull(Animator, name + " is missing animator component.");
        }

        public void AddAbility(CharacterAbility ability)
        {
            abilities.Add(ability);
        }

        public void RemoveAbility(CharacterAbility ability)
        {
            abilities.Remove(ability);
        }

        public void ChangeState(State<Character> newState)
        {
            stateMachine.ChangeState(newState);
        }
    }
}