using ArcticPass.AI;
using ArcticPass.Control;
using UnityEngine;

public class RabbitStateIdle : MonoBehaviour, IRabbitState
{
    [SerializeField] float maxWaitTime = 2f;
    [Range(0f, 1f)][SerializeField] float wonderChance = 0.5f;

    PlayerController player;
    IRabbitState fleeState;
    IRabbitState wonderState;

    float waitTime;

    private void Start()
    {
        player = PlayerController.GetPlayer();
        fleeState = GetComponent<RabbitStateFlee>();
        wonderState = GetComponent<RabbitStateWonder>();
    }

    public void OnStateEnter(AIRabbit rabbit)
    {
        waitTime = Random.Range(1f, maxWaitTime);
    }

    public void OnStateExit(AIRabbit rabbit)
    {
        
    }

    public void OnStateUpdate(AIRabbit rabbit)
    {
        RunTransitionTimer(rabbit);
        AvoidPlayer(rabbit);
    }

    private void RunTransitionTimer(AIRabbit rabbit)
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0f)
        {
            if (Random.value <= wonderChance)
            {
                rabbit.TransitionState(wonderState);
            }
            else
            {
                waitTime = Random.Range(1f, maxWaitTime);
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
}
