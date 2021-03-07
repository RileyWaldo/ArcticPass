using UnityEngine;

namespace CodeCabana.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        float maxHealth;
        bool isDead = false;

        private void Awake()
        {
            SetMaxHealth(health);
        }

        private void Die()
        {
            health = 0;
            isDead = true;
        }

        public void DealDamage(float damage)
        {
            if (isDead)
                return;

            health -= damage;
            if (health <= 0)
                Die();
        }

        public void Heal(float amount)
        {
            if (isDead)
                return;

            health += amount;
            if (health > maxHealth)
                health = maxHealth;
        }

        public void Revive(float startingHealth)
        {
            isDead = false;
            health = startingHealth;
        }

        public void SetMaxHealth(float amount)
        {
            maxHealth = amount;
        }

        public float GetHealth()
        {
            return health;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public float GetHealthFactor()
        {
            return health / maxHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}
