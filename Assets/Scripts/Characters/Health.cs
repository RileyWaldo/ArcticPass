using UnityEngine;

namespace ArcticPass.Control
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        float maxHealth = 100f;
        bool isDead = false;

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= Mathf.Epsilon)
            {
                health = 0f;
                Die();
            }
        }

        public void Heal(float value)
        {
            health += value;
            if (health > maxHealth)
                health = maxHealth;
        }

        public void SetMaxHealth(float value)
        {
            health = value;
            maxHealth = value;
        }

        public float GetHealth()
        {
            return health / maxHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void Die()
        {
            isDead = true;
        }
    }
}