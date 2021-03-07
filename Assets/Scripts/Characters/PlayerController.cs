using UnityEngine;
using UnityEngine.Assertions;
using CodeCabana.StateMachine;
using CodeCabana.Core;

namespace ArcticPass.Character
{
    public class PlayerController : StateMachine
    {
        Health health;
        Rigidbody2D rigidBody;
        Animator animator;

        private void Awake()
        {
            health = GetComponent<Health>();
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            Assert.IsNotNull(health, name + " is missing health component.");
            Assert.IsNotNull(rigidBody, name + " is missing rigidbody2D component.");
            Assert.IsNotNull(animator, name + " is missing animator component.");

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
    }
}