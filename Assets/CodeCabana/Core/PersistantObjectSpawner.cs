using UnityEngine;

namespace CodeCabana.Core
{
    public class PersistantObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistantObjectPrefab = default;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistantObjects();
            hasSpawned = true;
        }

        private void SpawnPersistantObjects()
        {
            GameObject persistantObject = Instantiate(persistantObjectPrefab);
            DontDestroyOnLoad(persistantObject);
        }
    }
}
