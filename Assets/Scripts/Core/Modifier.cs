using UnityEngine;

namespace ArcticPass.Core
{
    [System.Serializable]
    public class Modifier
    {
        public bool invulerable = false;
        public bool noHunger = false;
        public bool noTemp = false;
        public float attackRate = 0;
        public float attack = 0;
        public float defence = 0;
        public float speed = 0;

        public float timeLimit = 0;

        public void Update()
        {
            if (Expired())
                return;

            timeLimit -= Time.deltaTime;
        }

        public bool Expired()
        {
            return Mathf.Approximately(timeLimit, 0f);
        }

        public Modifier(float timeLimit)
        {
            this.timeLimit = timeLimit;
        }

        public static Modifier operator +(Modifier modifierA, Modifier modifierB)
        {
            Modifier sumModifier = new Modifier(0);
            sumModifier.attackRate = modifierA.attackRate + modifierB.attackRate;
            sumModifier.attack = modifierA.attack + modifierB.attack;
            sumModifier.defence = modifierA.defence + modifierB.defence;
            sumModifier.speed = modifierA.speed + modifierB.speed;
            sumModifier.invulerable = modifierA.invulerable || modifierB.invulerable;
            sumModifier.noHunger = modifierA.noHunger || modifierB.noHunger;
            sumModifier.noTemp = modifierA.noTemp || modifierB.noTemp;

            return sumModifier;
        }
    }
}
