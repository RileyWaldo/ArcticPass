using ArcticPass.AI;

public interface IRabbitState
{
    void OnStateEnter(AIRabbit rabbit);
    void OnStateExit(AIRabbit rabbit);
    void OnStateUpdate(AIRabbit rabbit);
}
