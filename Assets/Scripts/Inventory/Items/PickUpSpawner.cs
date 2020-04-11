using UnityEngine;

namespace ArcticPass.InventorySystem
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField] PickUp pickUpPrefab = null;

        public static PickUpSpawner spawner;

        private void Awake()
        {
            spawner = this;
        }

        public static PickUpSpawner GetSpawner()
        {
            return spawner;
        }

        public void Spawn(Item item, int amount)
        {
            PickUp pickUp = Instantiate(pickUpPrefab, transform);
            pickUp.SetItem(item, amount);
        }
    }
}
