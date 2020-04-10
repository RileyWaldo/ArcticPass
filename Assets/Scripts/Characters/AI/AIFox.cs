using UnityEditor;
using UnityEngine;
using ArcticPass.Control;

namespace ArcticPass.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AIFox : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] float moveSpeed = 1f;
        [SerializeField] float attackRange = 5f;

        [Header("Timers")]
        [Tooltip("In Seconds")] [SerializeField] float avoidTime = 2f;
        [Tooltip("In Seconds")] [SerializeField] float wonderTime = 2f;

        Health health;
        Rigidbody2D rigidBody;
        GameObject target = null;

        AIState state = AIState.Idle;
        Vector2 moveTo = Vector2.zero;

        float avoidTimer = 0f;
        float wonderTimer = 0f;

        private void Start()
        {
            health = GetComponent<Health>();
            rigidBody = GetComponent<Rigidbody2D>();
            target = PlayerController.GetPlayer().gameObject;
        }

        private void Update()
        {
            UpdateStates();
            switch (state)
            {
                case AIState.Idle:
                    Idle();
                    break;
                case AIState.Move:
                    Move();
                    break;
                case AIState.Attack:
                    Attack();
                    break;
                case AIState.Flee:
                    Flee();
                    break;
            }
        }

        private void UpdateStates()
        {
            if (target != null)
            {

            }
        }

        private void Idle()
        {
            if (target != null)
            {
                if (InRangeOfTarget())
                {
                    state = AIState.Flee;
                }
                else
                {
                    avoidTimer -= Time.deltaTime;
                    if (avoidTimer < 0f)
                        avoidTimer = 0f;
                }
            }
        }

        private void Move()
        {
            if (InRangeOfTarget())
            {
                avoidTimer += Time.deltaTime;
            }

            if (avoidTimer >= avoidTime)
            {
                state = AIState.Attack;
            }
        }

        private void Attack()
        {

        }

        private void Flee()
        {

        }

        private void Hit(float damage)
        {
            health.TakeDamage(damage);
        }

        private bool InRangeOfTarget()
        {
            if (Vector2.Distance(transform.position, target.transform.position) <= attackRange)
            {
                return true;
            }
            return false;
        }

        //gizmos
        private void OnDrawGizmosSelected()
        {
            //UnityEditor.Handles.color = Color.red;
            //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, attackRange);
        }
    }
}