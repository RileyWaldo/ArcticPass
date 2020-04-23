using ArcticPass.AI;
using ArcticPass.Control;
using UnityEngine;

public class RabbitStateFlee : MonoBehaviour, IRabbitState
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float fleeTime = 4f;

    PlayerController player;
    IRabbitState idleState;

    float fleeTimer = 0f;

    private void Start()
    {
        player = PlayerController.GetPlayer();
        idleState = GetComponent<RabbitStateIdle>();
    }

    public void OnStateEnter(AIRabbit rabbit)
    {
        fleeTimer = 0f;
    }

    public void OnStateExit(AIRabbit rabbit)
    {
        rabbit.RigidBody.velocity = Vector2.zero;
    }

    public void OnStateUpdate(AIRabbit rabbit)
    {
        RunTransitionTimer(rabbit);
        MoveAwayFromPlayer(rabbit);
    }

    private void RunTransitionTimer(AIRabbit rabbit)
    {
        if (Vector2.Distance(transform.position, player.transform.position) > rabbit.FleeRange)
        {
            fleeTimer += Time.deltaTime;
            if (fleeTimer >= fleeTime)
            {
                rabbit.TransitionState(idleState);
            }
        }
        else
        {
            fleeTimer = 0f;
        }
    }

    private void MoveAwayFromPlayer(AIRabbit rabbit)
    {
        Vector3 moveTo = (transform.position - player.transform.position);
        rabbit.RigidBody.velocity = moveTo.normalized * moveSpeed;
    }
}
