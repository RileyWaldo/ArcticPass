using UnityEngine;
using ArcticPass.InventorySystem;

namespace ArcticPass.UI
{
    public class InventoryUIUpdater : MonoBehaviour
    {
        [SerializeField] InventoryID inventoryID = InventoryID.player;
        [SerializeField] DragAndDrop itemSlotPrefab = null;

        Inventory inventory;

        private void Start()
        {
            inventory = Inventory.GetInventory(inventoryID);
            if (inventory != null)
            {
                inventory.UpdatedInventoryEvent += OnUpdateSlots;
            }

            PopulateUISlots();
            OnUpdateSlots();
        }

        private void Update()
        {
            OnUpdateSlots();
        }

        private void PopulateUISlots()
        {
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                DragAndDrop slot = Instantiate(itemSlotPrefab, transform);
                slot.SetUp(inventoryID, i);
            }
        }

        private void OnUpdateSlots()
        {
            int i = 0;
            foreach (Transform itemSlot in transform)
            {
                var slotID = itemSlot.GetComponent<DragAndDrop>();
                if (inventory.GetItem(i) != null)
                {
                    slotID.SetSprite(inventory.GetItem(i).GetSprite(), inventory.GetAmount(i));
                }
                else
                {
                    slotID.SetSprite(null, 0);
                }
                i++;
            }
        }
    }
}
