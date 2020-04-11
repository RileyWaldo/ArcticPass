using UnityEngine;

namespace ArcticPass.InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "ArcticPass/Create New Item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField] ItemID itemID = ItemID.rock;
        [SerializeField] Sprite sprite = null;
        [SerializeField] ItemType itemType = ItemType.Resource;
        [SerializeField] string itemName = "";
        [TextArea] [SerializeField] string itemDescription = "";
        [SerializeField] bool isStackable = false;
        [SerializeField] int value = 0;
        [SerializeField] int weight = 0;
        [SerializeField] int damage = 0;

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
