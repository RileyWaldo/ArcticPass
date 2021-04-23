using System;
using System.Collections.Generic;
using UnityEngine;
using CodeCabana.Core.Utils;
using CodeCabana.Saving;

namespace CodeCabana.Inventories
{
    public class Inventory : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        [SerializeField] List<InventorySlot> inventorySlots = new List<InventorySlot>();

        private float totalWeight = 0;

        public event Action onUpdateInventory;

        private void OnEnable()
        {
            onUpdateInventory += CalculateWeight;
        }

        private void OnDisable()
        {
            onUpdateInventory -= CalculateWeight;
        }

        public static Inventory GetPlayerInventory()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            return player.GetComponent<Inventory>();
        }

        private void CalculateWeight()
        {
            totalWeight = 0;
            foreach (InventorySlot slot in inventorySlots)
            {
                totalWeight += slot.Item.Weight * slot.Amount;
            }
        }

        public int GetSize()
        {
            return inventorySlots.Count;
        }

        public InventorySlot GetItemSlot(int slot)
        {
            if (inventorySlots[slot] == null)
                return null;
            return inventorySlots[slot];
        }

        public InventoryItem GetItemInSlot(int slot)
        {
            return inventorySlots[slot].Item;
        }

        public int GetItemAmountInSlot(int slot)
        {
            return inventorySlots[slot].Amount;
        }

        public float GetTotalWeight()
        {
            return totalWeight;
        }

        public bool AddItem(InventoryItem itemToAdd, int amount)
        {
            int maxStack = itemToAdd.MaxStack;
            if (maxStack < 0)
                maxStack = int.MaxValue;

            if (!itemToAdd.IsStackable)
            {
                maxStack = 1;
            }

            List<InventorySlot> slotsToAdd = new List<InventorySlot>();
            
            bool canAdd = false;
            foreach(InventorySlot slot in inventorySlots)
            {
                if(slot.Item == null)
                {
                    if (amount > maxStack)
                    {
                        amount -= maxStack;
                        slotsToAdd.Add(slot);
                        continue;
                    }

                    canAdd = true;
                    slot.Item = itemToAdd;
                    slot.Amount = amount;
                    break;
                }
                if(slot.Item == itemToAdd && slot.Item.IsStackable)
                {
                    if(slot.Amount + amount > maxStack)
                    {
                        amount -= maxStack - slot.Amount;
                        slotsToAdd.Add(slot);
                        continue;
                    }

                    canAdd = true;
                    slot.Amount += amount;
                    break;
                }
            }

            if(canAdd)
            {
                foreach(InventorySlot slot in slotsToAdd)
                {
                    slot.Item = itemToAdd;
                    slot.Amount = maxStack;
                }
            }

            onUpdateInventory?.Invoke();
            return canAdd;
        }

        public int ForceAddItem(InventoryItem itemToAdd, int amount)
        {
            int maxStack = itemToAdd.MaxStack;
            if (maxStack < 0)
                maxStack = int.MaxValue;

            if (!itemToAdd.IsStackable)
            {
                maxStack = 1;
            }

            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.Item == null)
                {
                    if (amount > maxStack)
                    {
                        slot.Item = itemToAdd;
                        slot.Amount = maxStack;
                        amount -= maxStack;
                        continue;
                    }

                    slot.Item = itemToAdd;
                    slot.Amount = amount;
                    amount = 0;
                    break;
                }
                if (slot.Item == itemToAdd && slot.Item.IsStackable)
                {
                    if (slot.Amount + amount > maxStack)
                    {
                        amount -= maxStack - slot.Amount;
                        slot.Amount = maxStack;
                        continue;
                    }

                    slot.Amount += amount;
                    amount = 0;
                    break;
                }
            }

            onUpdateInventory?.Invoke();

            return amount;
        }

        public bool RemoveItem(InventoryItem itemToRemove, int amount)
        {
            List<InventorySlot> slotToRemove = new List<InventorySlot>();

            bool canRemove = false;

            foreach(InventorySlot slot in inventorySlots)
            {
                if(slot.Item == itemToRemove)
                {
                    if(slot.Amount < amount)
                    {
                        amount -= slot.Amount;
                        slotToRemove.Add(slot);
                        continue;
                    }
                    if(slot.Amount >= amount)
                    {
                        canRemove = true;
                        slot.Amount -= amount;
                        if (slot.Amount == 0)
                            slot.Item = null;
                        break;
                    }
                }
            }

            if(canRemove)
            {
                foreach (InventorySlot slot in slotToRemove)
                {
                    slot.Item = null;
                    slot.Amount = 0;
                }
            }

            onUpdateInventory?.Invoke();
            return canRemove;
        }

        public bool AddItemToSlot(int slot, InventoryItem item, int amount)
        {
            bool canAdd = false;

            InventorySlot slotIndex = GetItemSlot(slot);

            if (slotIndex.Item == item && item.IsStackable && slotIndex.Amount + amount < item.MaxStack)
            {
                slotIndex.Amount += amount;
                canAdd = true;
            }
            if (slotIndex.Item == null && amount <= item.MaxStack)
            {
                slotIndex.Item = item;
                slotIndex.Amount = amount;
                canAdd = true;
            }

            return canAdd;
        }

        public bool RemoveItemFromSlot(int slot, InventoryItem item, int amount)
        {
            bool canRemove = false;

            InventorySlot slotIndex = GetItemSlot(slot);

            if(slotIndex.Item == item && slotIndex.Amount >= amount)
            {
                slotIndex.Amount -= amount;
                if (slotIndex.Amount <= 0)
                    slotIndex.Item = null;
                canRemove = true;
            }

            return canRemove;
        }

        public InventorySlot SwitchItemInSlot(InventorySlot item, int slot)
        {
            InventorySlot itemToReturn = inventorySlots[slot];
            inventorySlots[slot] = item;
            return itemToReturn;
        }

        public bool HasItem(InventoryItem item)
        {
            foreach(InventorySlot slot in inventorySlots)
            {
                if (slot.Item == item)
                    return true;
            }
            return false;
        }

        public bool HasItem(InventoryItem item, int amount)
        {
            int count = 0;
            foreach(InventorySlot slot in inventorySlots)
            {
                if(slot.Item == item)
                {
                    count += slot.Amount;
                    if (count < amount)
                        continue;
                    else
                        return true;
                }
            }
            return false;
        }

        object ISaveable.CaptureState()
        {
            List<object> state = new List<object>();
            foreach(InventorySlot slot in inventorySlots)
            {
                state.Add(slot.CaptureState());
            }
            return state;
        }

        void ISaveable.RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null)
                return;

            inventorySlots.Clear();
            foreach(object slot in stateList)
            {
                inventorySlots.Add(new InventorySlot(slot));
            }
            onUpdateInventory?.Invoke();
        }

        public bool? Evaluate(PredicateType predicate, string[] parameters)
        {
            switch(predicate)
            {
                case PredicateType.HasItem:
                    int amount = 1;
                    if (parameters.Length > 1)
                        amount = int.Parse(parameters[1]);

                    return HasItem(InventoryItem.GetFromID(parameters[0]), amount);
            }
            return null;
        }
    }
}
