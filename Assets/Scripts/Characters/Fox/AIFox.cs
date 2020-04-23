using UnityEngine;
using ArcticPass.Control;

namespace ArcticPass.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AIFox : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] float attackRange = 5f;

        Health health;
        Transform target;

        IFoxState currentState;
        IFoxState nextState;

        bool isStateTransitioning = false;

        public Rigidbody2D Rigidbody { get; private set; }

        private void Start()
        {
            health = GetComponent<Health>();
            Rigidbody = GetComponent<Rigidbody2D>();
            target = PlayerController.GetPlayer().transform;
            InitialState(GetComponent<FoxStateIdle>());
        }

        private void Update()
        {
            HandleStateEvents();
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

        private void InitialState(IFoxState state)
        {
            currentState = state;
            currentState.OnStateEnter(this);
        }

        public void Hit(float damage)
        {
            health.TakeDamage(damage);
        }

        public bool InRangeOfTarget()
        {
            if (Vector2.Distance(transform.position, target.transform.position) <= attackRange)
            {
                return true;
            }
            return false;
        }

        public void TransitionState(IFoxState state)
        {
            nextState = state;
            isStateTransitioning = true;
        }
    }
}