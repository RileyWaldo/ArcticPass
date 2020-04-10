using UnityEngine;

namespace ArcticPass.Core
{
    public class PersistantObjects : MonoBehaviour
    {
        [SerializeField] GameObject persistantObjectPrefab;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned)
            {
                return;
            }

            hasSpawned = true;
            GameObject persistantObjects = Instantiate(persistantObjectPrefab);
            DontDestroyOnLoad(persistantObjects);
        }
    }
}