using System.Collections.Generic;
using UnityEngine;

namespace ArcticPass.Core
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] float attackRate = 1;
        [SerializeField] float attack = 2;
        [SerializeField] float defence = 1;
        [SerializeField] float speed = 4;

        private List<Modifier> modifiers = new List<Modifier>();

        private void Update()
        {
            List<Modifier> tempList = new List<Modifier>();

            foreach(Modifier modifier in modifiers)
            {
                modifier.timeLimit -= Time.deltaTime;
                if(modifier.timeLimit <= 0)
                {
                    tempList.Add(modifier);
                }
            }

            foreach(Modifier modifier in tempList)
            {
                modifiers.Remove(modifier);
            }
        }

        private IStatModifier[] GetModifiers()
        {
            return GetComponents<IStatModifier>();
        }

        public float GetAttack()
        {
            var modifiers = GetModifiers();
            float value = attack;

            foreach(IStatModifier modifier in modifiers)
            {
                value += modifier.GetAttack();
            }

            return value;
        }

        public float GetDefence()
        {
            var modifiers = GetModifiers();
            float value = attack;

            foreach (IStatModifier modifier in modifiers)
            {
                value += modifier.GetDefence();
            }

            return value;
        }

        public float GetSpeed()
        {
            var modifiers = GetModifiers();
            float value = attack;

            foreach (IStatModifier modifier in modifiers)
            {
                value += modifier.GetSpeed();
            }

            return value;
        }

        public void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);
        }
    }
}
