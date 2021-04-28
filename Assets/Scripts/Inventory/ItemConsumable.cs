using UnityEngine;
using CodeCabana.Inventories;

namespace ArcticPass.Inventories
{
    public abstract class ItemConsumable : InventoryItem
    {
        [SerializeField] bool consumable = true;

        public bool Consumable
        {
            get { return consumable; }
        }

        public virtual void Use(GameObject user)
        {
            Debug.Log("You used an item!");
        }

        public bool IsConsumable()
        {
            return consumable;
        }
    }
}
