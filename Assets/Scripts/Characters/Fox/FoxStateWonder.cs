using UnityEngine;

namespace ArcticPass.AI
{
    public class FoxStateWonder : MonoBehaviour, IFoxState
    {
        [SerializeField] float moveSpeed = 2f;
        [SerializeField] float wonderRange = 4f;
        [SerializeField] float maxWaitTime = 4f;
        [Range(0f, 1f)]
        [SerializeField] float idleChance = 0.5f;

        IFoxState idleState;
        IFoxState fleeState;

        Vector3 goal;
        float waitTimer;

        private void Start()
        {
            idleState = GetComponent<FoxStateIdle>();
            fleeState = GetComponent<FoxStateFlee>();
        }

        public void OnStateEnter(AIFox fox)
        {
            waitTimer = Random.Range(1f, maxWaitTime);
            SetGoal();
        }

        public void OnStateExit(AIFox fox)
        {
            fox.Rigidbody.velocity = Vector2.zero;
        }

        public void OnStateUpdate(AIFox fox)
        {
            MoveToGoal(fox);
            AvoidPlayer(fox);
            RunTransitionTimer(fox);
        }

        private void RunTransitionTimer(AIFox fox)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                if (Random.value <= idleChance)
                {
                    fox.TransitionState(idleState);
                }
                else
                {
                    waitTimer = Random.Range(1f, maxWaitTime);
                }
            }
        }

        private void AvoidPlayer(AIFox fox)
        {
            if (fox.InRangeOfTarget())
            {
                fox.TransitionState(fleeState);
            }
        }

        private void MoveToGoal(AIFox fox)
        {
            if (Vector2.Distance(transform.position, goal) <= 0.1f)
            {
                Vector3 moveTo = goal - transform.position;
                fox.Rigidbody.velocity = moveTo.normalized * moveSpeed;
            }
            else
            {
                SetGoal();
            }
        }

        private void SetGoal()
        {
            goal = Random.insideUnitSphere * wonderRange;
            goal.z = transform.position.z;
        }
    }
}
