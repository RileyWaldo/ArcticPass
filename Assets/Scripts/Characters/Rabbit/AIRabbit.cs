using UnityEngine;
using ArcticPass.Control;

namespace ArcticPass.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    public class AIRabbit : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] float maxHealth = 20f;
        [SerializeField] float fleeRange = 4f;

        IRabbitState currentState;
        IRabbitState nextState;

        bool isStateTransitioning = false;

        public Rigidbody2D RigidBody { get; private set; }
        public Animator Animator { get; private set; }
        public Health Health { get; private set; }
        public float FleeRange { get { return fleeRange; } }

        private void Start()
        {
            Health = GetComponent<Health>();
            Health.SetMaxHealth(maxHealth);
            Animator = GetComponentInChildren<Animator>();
            RigidBody = GetComponent<Rigidbody2D>();
            InitialState(GetComponent<RabbitStateIdle>());
        }

        private void Update()
        {
            HandleStateEvents();
            Animate();
        }

        private void HandleStateEvents()
        {
            currentState.OnStateUpdate(this);
            if (isStateTransitioning)
            {
                isStateTransitioning = false;
                currentState.OnStateExit(this);
                currentState = nextState;
                currentState.OnStateEnter(this);
            }
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

        private void InitialState(IRabbitState state)
        {
            currentState = state;
            currentState.OnStateEnter(this);
        }

        public void TransitionState(IRabbitState state)
        {
            nextState = state;
            isStateTransitioning = true;
        }
    }
}
