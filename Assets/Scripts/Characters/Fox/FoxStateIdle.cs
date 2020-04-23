using UnityEngine;

namespace ArcticPass.AI
{
    public class FoxStateIdle : MonoBehaviour, IFoxState
    {
        [SerializeField] float maxWaitTime = 3f;
        [Range(0f, 1f)]
        [SerializeField] float wonderChance = 0.5f;

        IFoxState wonderState;

        float waitTimer;

        private void Start()
        {
            wonderState = GetComponent<FoxStateWonder>();
        }

        public void OnStateEnter(AIFox fox)
        {
            waitTimer = Random.Range(1f, maxWaitTime);
        }

        public void OnStateExit(AIFox fox)
        {

        }

        public void OnStateUpdate(AIFox fox)
        {
            RunTransitionTimer(fox);
            AvoidPlayer(fox);
        }

        private void RunTransitionTimer(AIFox fox)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                if (Random.value <= wonderChance)
                {
                    fox.TransitionState(wonderState);
                }
                else
                {
                    waitTimer = Random.Range(1f, maxWaitTime);
                }
            }
        }

        private static void AvoidPlayer(AIFox fox)
        {
            if (fox.InRangeOfTarget())
            {
                //fox.TransitionState();
            }
        }
    }
}
