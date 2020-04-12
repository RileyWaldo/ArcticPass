using ArcticPass.Control;
using ArcticPass.Core;
using UnityEngine;

namespace ArcticPass.AI
{
    public class Bird : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 1f;
        [SerializeField] float fleeSpeed = 3f;
        [Tooltip("How close the player can get.")]
        [SerializeField] float fleeRange = 4f;
        [Tooltip("How far bird can wonder.")]
        [SerializeField] float wonderRange = 6f;
        [Tooltip("Max time to transition between states.")]
        [SerializeField] float stateTime = 4f;

        Animator animator;

        const string _isPecking = "isPecking";
        const string _fly = "fly";

        Vector2 startPos;
        Vector3 goal;
        AIState state = AIState.Idle;
        float stateTimer = 0f;
        bool isPecking = false;
        bool isFleeing = false;
        float peckTimer = 2f;

        private void Start()
        {
            startPos = transform.position;
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            HandleState();
            switch(state)
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

        private void HandleState()
        {
            if (isFleeing) { return; }

            stateTimer -= Time.deltaTime;
            if (stateTimer <= 0)
            {
                stateTimer = Random.Range(1f, stateTime);
                if (Random.value < 0.5f)
                {
                    state = AIState.Idle;
                }
                else
                {
                    state = AIState.Move;
                    goal = startPos + (Random.insideUnitCircle * wonderRange);
                }
            }

            Vector3 player = PlayerController.GetPlayer().transform.position;
            if (Vector2.Distance(transform.position, player) < fleeRange)
            {
                state = AIState.Flee;
                isFleeing = true;

                GetComponentInChildren<Collider2D>().enabled = false;
                GetComponentInChildren<DepthSorting>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().sortingOrder = 32767;
                
                float direction = 1f;
                if (Random.value < 0.5f)
                {
                    direction = -1f;
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                goal = new Vector3(direction, 1, 0);
                goal.Normalize();
            }
        }

        private void Idle()
        {
            animator.SetBool(_isPecking, true);
            peckTimer -= Time.deltaTime;

            if(peckTimer <= 0f)
            {
                peckTimer = Random.Range(1f, 2f);
                isPecking = !isPecking;
            }

            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (isPecking && info.normalizedTime >= 1f)
            {
                animator.Play(info.tagHash);
            }
        }

        private void Move()
        {
            animator.SetBool(_isPecking, false);

            if(Vector3.Distance(transform.position, goal) < 0.1f)
            {
                goal = startPos + (Random.insideUnitCircle * wonderRange);
            }

            Vector3 moveDirection = goal - transform.position;
            float xScale = 1;
            if (moveDirection.x > 0)
                xScale = -1;
            transform.localScale = new Vector3(xScale, 1, 1);
            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
        }

        private void Flee()
        {
            animator.SetBool(_fly, true);
            transform.position += goal * fleeSpeed * Time.deltaTime;

            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

            if (!GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider2D>().bounds))
            {
                Destroy(gameObject);
            }
        }

        //gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, fleeRange);
        }
    }
}
