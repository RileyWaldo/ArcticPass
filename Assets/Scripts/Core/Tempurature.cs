using UnityEngine;
using CodeCabana.Core;

namespace ArcticPass.Core
{
    public class Tempurature : MonoBehaviour, StatModifier
    {
        [SerializeField] float maxTemp = 100f;
        [SerializeField] float speedPenalty = -1.5f;

        Health health;

        float temp;

        private void Awake()
        {
            health = GetComponent<Health>();
            temp = maxTemp;
        }

        public void WarmUp(float amount)
        {
            temp += amount;
            if (temp > maxTemp)
                temp = maxTemp;
        }

        public float GetAttack()
        {
            return 0;
        }

        public float GetDefence()
        {
            return 0;
        }

        public float GetSpeed()
        {
            if (temp <= 0)
                return speedPenalty;
            return 0;
        }
    }
}
