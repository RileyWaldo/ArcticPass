using UnityEngine;

namespace ArcticPass.InventorySystem
{
    [System.Serializable]
    public class ItemSlot
    {
        [SerializeField] Item item;
        [SerializeField] int amount;

        public ItemSlot(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void SetItem(Item item)
        {
            this.item = item;
        }

        public void SetItem(int amount)
        {
            this.amount = amount;
        }

        public void SetItem(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void AddAmount(int amount)
        {
            this.amount += amount;
        }

        public Item GetItem()
        {
            return item;
        }

        public int GetAmount()
        {
            return amount;
        }

        public void RemoveItem()
        {
            item = null;
            amount = 0;
        }
    }
}
