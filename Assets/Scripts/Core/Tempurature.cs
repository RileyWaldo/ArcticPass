using UnityEngine;
using CodeCabana.Core;

namespace ArcticPass.Core
{
    public class Tempurature : MonoBehaviour, IStatModifier
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

        public Modifier GetModifier()
        {
            Modifier modifier = new Modifier(0);

            if (temp <= 0)
                modifier.speed = speedPenalty;

            return modifier;
        }
    }
}
