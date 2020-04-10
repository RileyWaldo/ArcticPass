using UnityEngine;

namespace ArcticPass.InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "ArcticPass/Create New Item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField] ItemID itemID;
        [SerializeField] Sprite sprite;
        [SerializeField] ItemType itemType;
        [SerializeField] string itemName;
        [TextArea] [SerializeField] string itemDescription;
        [SerializeField] bool isStackable;
        [SerializeField] int value;
        [SerializeField] int weight;
        [SerializeField] int damage;

        public ItemID GetItemID()
        {
            return itemID;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        public ItemType GetItemType()
        {
            return itemType;
        }

        public string GetName()
        {
            return itemName;
        }

        public string GetDescription()
        {
            return itemDescription;
        }

        public int GetValue()
        {
            return value;
        }

        public int GetWeight()
        {
            return weight;
        }

        public int GetDamage()
        {
            return damage;
        }

        public bool IsStackable()
        {
            return isStackable;
        }
    }
}
