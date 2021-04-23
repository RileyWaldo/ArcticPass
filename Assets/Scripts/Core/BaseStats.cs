using UnityEngine;

namespace ArcticPass.Core
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] float attackRate = 1;
        [SerializeField] float attack = 2;
        [SerializeField] float defence = 1;
        [SerializeField] float speed = 4;

        private StatModifier[] GetModifiers()
        {
            return GetComponents<StatModifier>();
        }

        public float GetAttack()
        {
            var modifiers = GetModifiers();
            float value = attack;

            foreach(StatModifier modifier in modifiers)
            {
                value += modifier.GetAttack();
            }

            return value;
        }

        public float GetDefence()
        {
            var modifiers = GetModifiers();
            float value = attack;

            foreach (StatModifier modifier in modifiers)
            {
                value += modifier.GetDefence();
            }

            return value;
        }

        public float GetSpeed()
        {
            var modifiers = GetModifiers();
            float value = attack;

            foreach (StatModifier modifier in modifiers)
            {
                value += modifier.GetSpeed();
            }

            return value;
        }
    }
}
