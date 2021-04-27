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

        public Modifier(float timeLimit)
        {
            this.timeLimit = timeLimit;
        }
    }
}
