using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcticPass.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] InventoryID inventoryID = InventoryID.player;
        [SerializeField] ItemSlot[] itemSlots = { };

        static Dictionary<InventoryID, Inventory> inventories = new Dictionary<InventoryID, Inventory>();

        public static Inventory GetInventory(InventoryID key)
        {
            if (inventories.ContainsKey(key))
            {
                return inventories[key];
            }
            else
            {
                return null;
            }
        }

        private void Awake()
        {
            AddInstanceReferance();
            AddItemToInventory(ItemFinder.FindItem(ItemID.furYeti), 1);
            AddItemToSlot(ItemFinder.FindItem(ItemID.rope), 1, 1);
        }

        private void AddInstanceReferance()
        {
            inventories.Add(inventoryID, this);
        }

        public bool AddItemToInventory(Item itemToAdd, int amount)
        {
            if (!itemToAdd.IsStackable())
            {
                amount = 1;
            }
            bool canAdd = false;
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].GetItem() == itemToAdd)
                {
                    if (itemToAdd.IsStackable())
                    {
                        itemSlots[i].AddAmount(amount);
                        canAdd = true;
                        break;
                    }
                }
                if (itemSlots[i].GetItem() == null)
                {
                    itemSlots[i].SetItem(itemToAdd, amount);
                    canAdd = true;
                    break;
                }
            }

            return canAdd;
        }

        public bool AddItemToSlot(Item itemToAdd, int value, int slot)
        {
            if (!itemToAdd.IsStackable())
            {
                value = 1;
            }
            bool canAdd = false;
            Item itemToCheck = itemSlots[slot].GetItem();
            if (itemToCheck == itemToAdd && itemToCheck.IsStackable())
            {
                itemSlots[slot].SetItem(value);
                canAdd = true;
            }
            else if (itemToCheck == null)
            {
                itemSlots[slot].SetItem(itemToAdd, value);
                canAdd = true;
            }

            return canAdd;
        }

        public void SetItemInSlot(Item item, int amount, int slot)
        {
            itemSlots[slot].SetItem(item, amount);
        }

        public void RemoveItemFromSlot(int slot)
        {
            itemSlots[slot].RemoveItem();
        }

        public Item GetItem(int slot)
        {
            return itemSlots[slot].GetItem();
        }

        public int GetAmount(int slot)
        {
            return itemSlots[slot].GetAmount();
        }

        public int GetSize()
        {
            return itemSlots.Length;
        }

        public void AddAmountInSlot(int amount, int slot)
        {
            itemSlots[slot].AddAmount(amount);
        }

    }
}
