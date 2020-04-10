using UnityEngine;

namespace ArcticPass.Core
{
    public class Storm : MonoBehaviour
    {
        [SerializeField] float stormProgress = -1000f;
        [SerializeField] float playerProgress = 100f;
        [SerializeField] float stormSpeed = 1f;

        bool stormIsPaused = false;

        static Storm storm;

        public static Storm GetStorm()
        {
            return storm;
        }

        private void Awake()
        {
            storm = this;
        }

        private void Update()
        {
            if (!stormIsPaused)
            {
                stormProgress += stormSpeed * Time.deltaTime;
            }
        }

        public float StormProgress { get; set; }

        public float PlayerProgress { get; set; }

        public void AdvancePlayer(float progress)
        {
            playerProgress += progress;
        }
    }
}
