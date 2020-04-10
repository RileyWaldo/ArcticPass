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
        Rigidbody2D rigidBody;
        GameObject target = null;

        const string horizontal = "Horizontal";
        const string vertical = "Vertical";

        //states

        AIState state = AIState.Idle;
        Vector3 goal = Vector3.zero;
        float timerMoving = 0f;
        float timerFlee = 0f;

        //Unity functions

        private void Start()
        {
            health = GetComponent<Health>();
            health.SetMaxHealth(maxHealth);
            animator = GetComponentInChildren<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
            target = PlayerController.GetPlayer().gameObject;
            goal = transform.position;
        }

        private void Update()
        {
            ProcessState();
            Animate();
        }

        /// private methods

        private void ProcessState()
        {
            switch (state)
            {
                case AIState.Idle:
                    Idle();
                    break;
                case AIState.Move:
                    Move();
                    break;
                case AIState.Flee:
                    Flee();
                    break;
            }
        }

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

        private void Move()
        {
            //rigidBody.velocity = goal * stats.GetSpeed();
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

        private void Animate()
        {
            animator.SetFloat(horizontal, rigidBody.velocity.normalized.x);
            animator.SetFloat(vertical, rigidBody.velocity.normalized.y);
        }

        private void MoveToGoal(float speed)
        {
            if(Vector2.Distance(transform.position, goal) <= speed * Time.deltaTime)
            {
                rigidBody.velocity = Vector2.zero;
            }
            else
            {
                Vector2 moveDirection = goal - transform.position;
                rigidBody.AddForce(moveDirection * speed);
                rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, speed);
            }
        }

        //gizmos
        private void OnDrawGizmosSelected()
        {
            //Handles.color = Color.green;
            //Handles.DrawWireDisc(transform.position, Vector3.forward, fleeRange);
            //Handles.DrawLine(transform.position, goal);
        }
    }
}
