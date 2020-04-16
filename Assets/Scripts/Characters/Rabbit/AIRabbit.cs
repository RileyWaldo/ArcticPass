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
        [SerializeField] float moveSpeed = 2f;
        [SerializeField] float runSpeed = 4f;
        [SerializeField] float fleeRange = 5f;
        [Header ("Timers")]
        [Tooltip("In Seconds")]
        [SerializeField] float moveWaitTime = 2f;
        [Range(0f, 1f)]
        [SerializeField] float randomMoveChance = 0.5f;
        [Tooltip("In Seconds")]
        [SerializeField] float fleeTime = 4f;

        //cached refrance

        Health health;
        Animator animator;
        GameObject target = null;

        const string horizontal = "Horizontal";
        const string vertical = "Vertical";

        //states

        AIState state = AIState.Idle;
        Vector3 goal = Vector3.zero;
        float timerMoving = 0f;
        float timerFlee = 0f;

        IRabbitState currentState;

        //properties

        public Rigidbody2D RigidBody { get; private set; }

        //Unity functions

        private void Start()
        {
            health = GetComponent<Health>();
            health.SetMaxHealth(maxHealth);
            animator = GetComponentInChildren<Animator>();
            RigidBody = GetComponent<Rigidbody2D>();
            target = PlayerController.GetPlayer().gameObject;
            goal = transform.position;

            currentState = new RabbitStateIdle();
        }

        private void Update()
        {
            currentState.OnStateUpdate(this);
        }

        /// private methods

        private void Idle()
        {
            timerMoving += Time.deltaTime;
            if (timerMoving >= moveWaitTime)
            {
                timerMoving = 0f;
                if (Random.value > randomMoveChance)
                {
                    goal = transform.position + Random.insideUnitSphere * fleeRange;
                }
            }
            MoveToGoal(moveSpeed);

            if(Vector2.Distance(transform.position, target.transform.position) < fleeRange)
            {
                state = AIState.Flee;
            }
        }

        private void Flee()
        {
            goal = transform.position * 2f - target.transform.position;
            MoveToGoal(runSpeed);

            if(Vector2.Distance(transform.position, target.transform.position) > fleeRange)
            {
                timerFlee += Time.deltaTime;
                if(timerFlee >= fleeTime)
                {
                    timerFlee = 0f;
                    state = AIState.Idle;
                }
            }
            else
            {
                timerFlee = 0f;
            }
        }

        private void MoveToGoal(float speed)
        {
            if(Vector2.Distance(transform.position, goal) <= speed * Time.deltaTime)
            {
                RigidBody.velocity = Vector2.zero;
            }
            else
            {
                Vector2 moveDirection = goal - transform.position;
                RigidBody.AddForce(moveDirection * speed);
                RigidBody.velocity = Vector3.ClampMagnitude(RigidBody.velocity, speed);
            }
        }
    }
}
