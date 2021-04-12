using System.Collections.Generic;
using UnityEngine;

namespace ArcticPass.Core
{
    public class SledController : MonoBehaviour
    {
        [SerializeField] int level = 1;
        [SerializeField] float speed = 4;
        [SerializeField] int maxDogs = 4;

        public int GetLevel()
        {
            return level;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public int GetMaxDogs()
        {
            return maxDogs;
        }
    }
}
