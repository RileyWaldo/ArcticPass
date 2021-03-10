using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;
using CodeCabana.StateMachine;
using CodeCabana.Core;

namespace ArcticPass.Character
{
    public class PlayerController : StateMachine
    {
        InputMaster input;

        public Health Health { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }
        public Animator Animator { get; private set; }

        private void Awake()
        {
            SetUpInput();

            Health = GetComponent<Health>();
            RigidBody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();

            Assert.IsNotNull(Health, name + " is missing health component.");
            Assert.IsNotNull(RigidBody, name + " is missing rigidbody2D component.");
            Assert.IsNotNull(Animator, name + " is missing animator component.");

            AwakeState();
        }

        private void Start()
        {
            StartState();
        }

        private void Update()
        {
            UpdateState();
        }

        private void OnEnable()
        {
            input.Player.Enable();
        }

        private void OnDisable()
        {
            input.Player.Disable();
        }

        private void SetUpInput()
        {
            input = new InputMaster();
            input.Player.Attack.performed += context => OnAttack();
        }

        private void OnAttack()
        {
            Debug.Log("Input test attack!");
        }
    }
}