using UnityEditor;
using UnityEngine;
using ArcticPass.Control;

namespace ArcticPass.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    public class AIRabbit : MonoBehaviour
    {
        //Tunables
        [Header("Stats")]
        [SerializeField] float maxHealth = 20f;
        [SerializeField] float damage = 2f;

        IRabbitState currentState;

        //properties

        public Rigidbody2D RigidBody { get; private set; }
        public Animator Animator { get; private set; }
        public Health Health { get; private set; }

        //Unity functions

        private void Start()
        {
            Health = GetComponent<Health>();
            Health.SetMaxHealth(maxHealth);
            Animator = GetComponentInChildren<Animator>();
            RigidBody = GetComponent<Rigidbody2D>();
            InitialState(GetComponent<RabbitStateIdle>());
        }

        private void InitialState(IRabbitState state)
        {
            currentState = state;
            currentState.OnStateEnter(this);
        }

        private void Update()
        {
            currentState.OnStateUpdate(this);
            Animate();
        }

        private void Animate()
        {
            Animator.SetFloat("Horizontal", RigidBody.velocity.normalized.x);
            Animator.SetFloat("Vertical", RigidBody.velocity.normalized.y);
            if (RigidBody.velocity.magnitude <= Mathf.Epsilon)
            {
                Animator.speed = 0f;
            }
            else
            {
                Animator.speed = 1f;
            }
        }

        public void TransitionState(IRabbitState state)
        {
            print("State Transition: " + state);
            currentState.OnStateExit(this);
            currentState = state;
            currentState.OnStateEnter(this);
        }
    }
}
