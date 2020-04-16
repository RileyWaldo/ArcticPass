using ArcticPass.AI;
using ArcticPass.Control;
using UnityEngine;

public class RabbitStateFlee : MonoBehaviour, IRabbitState
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float fleeRange = 5f;
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
        if(Vector2.Distance(transform.position, player.transform.position) > fleeRange)
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

        Vector3 moveTo = (transform.position - player.transform.position);
        rabbit.RigidBody.velocity = moveTo.normalized * moveSpeed;
    }
}
