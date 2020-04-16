using ArcticPass.AI;
using ArcticPass.Control;
using UnityEngine;

public class RabbitStateIdle : MonoBehaviour, IRabbitState
{
    [SerializeField] float fleeRange = 4f;
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
        waitTime -= Time.deltaTime;
        if(waitTime <= 0f)
        {
            if(Random.value <= wonderChance)
            {
                rabbit.TransitionState(wonderState);
            }
            else
            {
                waitTime = Random.Range(1f, maxWaitTime);
            }
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= fleeRange)
        {
            rabbit.TransitionState(fleeState);
        }
    }
}
