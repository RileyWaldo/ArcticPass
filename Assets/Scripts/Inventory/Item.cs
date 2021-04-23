using UnityEngine;
using CodeCabana.Inventories;

namespace ArcticPass.Inventories
{
    public abstract class Item : InventoryItem
    {
        [SerializeField] float weight = 0;
    }
}
