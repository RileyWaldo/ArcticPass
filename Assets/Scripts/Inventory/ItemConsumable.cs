using UnityEngine;
using CodeCabana.Inventories;

namespace ArcticPass.Inventories
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ArcticPass/Create item/Consumable", order = 0)]
    public class ItemConsumable : InventoryItem
    {
        [SerializeField] bool consumable = true;

        public virtual void Use(GameObject user)
        {
            Debug.Log("You used an item!");
        }
    }
}
