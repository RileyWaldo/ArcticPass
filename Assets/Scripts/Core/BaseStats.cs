using System.Collections.Generic;
using UnityEngine;
using CodeCabana.Core;

namespace ArcticPass.Core
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] float attackRate = 1;
        [SerializeField] float attack = 2;
        [SerializeField] float defence = 1;
        [SerializeField] float speed = 4;

        private Health health;

        private List<Modifier> modifiers = new List<Modifier>();

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            bool cleanUp = false;
            List<Modifier> tempList = new List<Modifier>();

            foreach(Modifier modifier in modifiers)
            {
                modifier.Update();
                if(modifier.Expired())
                {
                    cleanUp = true;
                    tempList.Add(modifier);
                }
            }

            if(cleanUp)
            {
                foreach (Modifier modifier in tempList)
                {
                    if(modifier.invulerable)
                    {
                        bool invulnerable = false;
                        foreach(Modifier modifier1 in modifiers)
                        {
                            if(modifier1.invulerable)
                            {
                                invulnerable = true;
                                break;
                            }
                        }
                        health.Invincible(invulnerable);
                    }
                    modifiers.Remove(modifier);
                }
            }
        }

        private IStatModifier[] GetModifiers()
        {
            return GetComponents<IStatModifier>();
        }

        public Modifier GetStats()
        {
            Modifier modifier = new Modifier(0);
            modifier.attackRate += attackRate;
            modifier.attack += attack;
            modifier.defence += defence;
            modifier.speed += speed;

            foreach(Modifier tempModifier in modifiers)
            {
                modifier += tempModifier;
            }

            return modifier;
        }

        public void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);
            if (modifier.invulerable)
                health.Invincible(true);
        }
    }
}
