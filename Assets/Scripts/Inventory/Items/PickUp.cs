using UnityEngine;

namespace ArcticPass.InventorySystem
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] Item item;
        [SerializeField] int amount = 1;

        SpriteRenderer render;

        private void Awake()
        {
            render = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            if (item != null)
            {
                render.sprite = item.GetSprite();
            }
        }

        private void OnMouseDown()
        {
            Inventory.GetInventory(InventoryID.player).AddItemToInventory(item, amount);
            Destroy(gameObject);
        }

        public void SetItem(Item item, int amount)
        {
            if (item == null) { return; }

            this.item = item;
            this.amount = amount;
            render.sprite = item.GetSprite();
        }
    }
}
