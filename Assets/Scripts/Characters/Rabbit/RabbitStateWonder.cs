using ArcticPass.Control;
using UnityEngine;

namespace ArcticPass.AI
{
    public class RabbitStateWonder : MonoBehaviour, IRabbitState
    {
        [SerializeField] float moveSpeed = 2f;
        [SerializeField] float maxWaitTime = 2f;
        [SerializeField] float wonderDistance = 5f;
        [Range(0f, 1f)] [SerializeField] float idleChance = 0.5f;

        PlayerController player;
        IRabbitState idleState;
        IRabbitState fleeState;

        Vector3 goal;
        float waitTime = 0f;

        private void Start()
        {
            player = PlayerController.GetPlayer();
            idleState = GetComponent<RabbitStateIdle>();
            fleeState = GetComponent<RabbitStateFlee>();
        }

        public void OnStateEnter(AIRabbit rabbit)
        {
            waitTime = Random.Range(1f, maxWaitTime);
            SetGoal();
        }

        public void OnStateExit(AIRabbit rabbit)
        {
            rabbit.RigidBody.velocity = Vector2.zero;
        }

        public void OnStateUpdate(AIRabbit rabbit)
        {
            RunTransitionTimer(rabbit);
            AvoidPlayer(rabbit);
            MoveToGoal(rabbit);
        }

        private void RunTransitionTimer(AIRabbit rabbit)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0f)
            {
                waitTime = Random.Range(1f, maxWaitTime);
                if (Random.value <= idleChance)
                {
                    rabbit.TransitionState(idleState);
                }
            }
        }

        private void AvoidPlayer(AIRabbit rabbit)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= rabbit.FleeRange)
            {
                rabbit.TransitionState(fleeState);
            }
        }

        private void MoveToGoal(AIRabbit rabbit)
        {
            if (Vector2.Distance(transform.position, goal) > 0.1f)
            {
                Vector3 moveTo = goal - transform.position;
                rabbit.RigidBody.velocity = moveTo.normalized * moveSpeed;
            }
            else
            {
                SetGoal();
            }
        }

        private void SetGoal()
        {
            Vector3 ranPos = Random.insideUnitSphere;
            ranPos.z = 0f;
            ranPos *= wonderDistance;
            goal = transform.position + ranPos;
        }
    }
}
