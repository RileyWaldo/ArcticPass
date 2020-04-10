using System.Collections.Generic;
using UnityEngine;

namespace ArcticPass.InventorySystem
{
    public class ItemFinder : MonoBehaviour
    {
        [SerializeField] Item[] items;

        static Dictionary<ItemID, Item> itemLookUp = new Dictionary<ItemID, Item>();

        private void Awake()
        {
            PopulateLookUp();
        }

        private void PopulateLookUp()
        {
            foreach (Item item in items)
            {
                if (!itemLookUp.ContainsKey(item.GetItemID()))
                {
                    itemLookUp.Add(item.GetItemID(), item);
                }
            }
        }

        public static Item FindItem(ItemID iD)
        {
            return itemLookUp[iD];
        }
    }
}
