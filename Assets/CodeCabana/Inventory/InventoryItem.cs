using System.Collections.Generic;
using UnityEngine;

namespace CodeCabana.Inventories
{
    public abstract class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {
        [Tooltip("Unique item ID, clear this field to generate a new one.")]
        [SerializeField] string itemID = "";
        [Tooltip("Item's icon sprite.")]
        [SerializeField] Sprite icon = default;
        [Tooltip("Item's name.")]
        [SerializeField] string itemName = "";
        [TextArea(4, 5)][Tooltip("Item's description.")]
        [SerializeField] string itemDescription = "";
        [Tooltip("Item's value.")]
        [SerializeField] float value = 0;
        [Tooltip("Item's weight.")]
        [SerializeField] float weight = 0;
        [Tooltip("Can this item stack?")]
        [SerializeField] bool isStackable = false;
        [Tooltip("Set this less than 0 if there is no limit.")]
        [SerializeField] int maxStack = 1;

        static Dictionary<string, InventoryItem> itemLookUpCache;

        public static InventoryItem GetFromID(string itemID)
        {
            if(itemLookUpCache == null)
            {
                itemLookUpCache = new Dictionary<string, InventoryItem>();
                InventoryItem[] itemList = Resources.LoadAll<InventoryItem>("");
                foreach(InventoryItem item in itemList)
                {
                    if(itemLookUpCache.ContainsKey(item.ItemID))
                    {
                        InventoryItem itemInLookUp = itemLookUpCache[item.ItemID];
                        Debug.LogError($"Looks like there's a duplicate itemID. \n" + itemInLookUp.ItemID + "\n" + itemInLookUp.Name + "\n" + item.Name);
                        continue;
                    }

                    itemLookUpCache[item.ItemID] = item;
                }
            }

            if (itemID == null || !itemLookUpCache.ContainsKey(itemID))
                return null;

            return itemLookUpCache[itemID];
        }

        public string ItemID
        {
            get { return itemID; }
        }
        
        public Sprite Icon
        {
            get { return icon; }
        }

        public string Name
        {
            get { return itemName; }
        }

        public string Description
        {
            get { return itemDescription; }
        }

        public float Value
        {
            get { return value; }
        }

        public float Weight
        {
            get { return weight; }
        }

        public bool IsStackable
        {
            get { return isStackable; }
        }

        public int MaxStack
        {
            get { return maxStack; }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //not used
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if(string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }
    }
}
