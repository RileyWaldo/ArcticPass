using UnityEngine;
using CodeCabana.Core;

namespace ArcticPass.Core
{
    public class Hunger : MonoBehaviour, IStatModifier
    {
        [SerializeField] float maxHunger = 100;
        [SerializeField] float speedPenalty = -1.5f;

        Health health;

        float hunger;

        private void Awake()
        {
            health = GetComponent<Health>();
            hunger = maxHunger;
        }

        public void Eat(float amount)
        {
            hunger += amount;
            if (hunger > maxHunger)
                hunger = maxHunger;
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
            if (hunger <= 0)
                return speedPenalty;
            return 0;
        }
    }
}
