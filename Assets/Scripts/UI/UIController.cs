using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ArcticPass.InventorySystem;

namespace ArcticPass.UI
{
    public class UIController : MonoBehaviour
    {
        //Referances
        [Header("UI")]
        [SerializeField] Image mouseItemIcon;
        [SerializeField] Text mouseItemText;

        [Header("Menu Referance")]
        [SerializeField] GameObject playerMenu;
        [SerializeField] GameObject sledMenu;

        [Header("Inventorys")]
        [SerializeField] InventoryID playerInventoryID;
        [SerializeField] InventoryID sledInventoryID;
        [SerializeField] Text descriptionText;

        //States

        Inventory playerInventory;
        Inventory sledInventory;
        ItemSlot itemMouse = new ItemSlot(null, 0);
        Vector3 itemMouseOffset = Vector2.zero;

        private void Start()
        {
            playerInventory = Inventory.GetInventory(playerInventoryID);
            sledInventory = Inventory.GetInventory(sledInventoryID);
            HideAllMenus();
        }

        private void Update()
        {
            UpdateMouseSprite();
            UpdateUI();
            ProcessInput();
        }

        // Private Methods

        private void UpdateUI()
        {
            descriptionText.text = "";
        }

        private void ProcessInput()
        {
            //hot keys
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                sledMenu.SetActive(!sledMenu.activeSelf);
                sledInventory.ForceUpdate();
            }

            //mouse input
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(eventData, raycastResults);

            foreach (RaycastResult hit in raycastResults)
            {
                var mouseHoverObject = hit.gameObject.GetComponent<DragAndDrop>();
                if (mouseHoverObject != null)
                {
                    Item item;
                    if (mouseHoverObject.GetInventoryID() == InventoryID.player)
                        item = playerInventory.GetItem(mouseHoverObject.GetSlotID());
                    else
                        item = sledInventory.GetItem(mouseHoverObject.GetSlotID());
                    if (item != null)
                    {
                        descriptionText.text = item.GetDescription();
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                ProcessClick(raycastResults);
            }
        }

        private void ProcessClick(List<RaycastResult> raycastResults)
        {

            foreach (RaycastResult hit in raycastResults)
            {
                var clickedObject = hit.gameObject.GetComponent<DragAndDrop>();
                if (clickedObject != null)
                {
                    itemMouseOffset = Input.mousePosition - clickedObject.transform.position;
                    ProcessItemSlot(clickedObject);
                }
            }

            if (raycastResults.Count == 0)
            {
                DropItemInMouse();
            }
        }

        private void DropItemInMouse()
        {
            if (itemMouse.GetItem() == null) { return; }

            PickUpSpawner.GetSpawner().Spawn(itemMouse.GetItem(), itemMouse.GetAmount());
            itemMouse.RemoveItem();
        }

        private void UpdateMouseSprite()
        {
            if (itemMouse.GetItem() != null)
            {
                mouseItemIcon.enabled = true;
                mouseItemText.enabled = true;
                mouseItemIcon.transform.position = Input.mousePosition - itemMouseOffset;
                mouseItemIcon.sprite = itemMouse.GetItem().GetSprite();
                if (itemMouse.GetAmount() > 1)
                    mouseItemText.text = itemMouse.GetAmount().ToString();
                else
                    mouseItemText.text = "";
            }
            else
            {
                mouseItemIcon.enabled = false;
                mouseItemText.enabled = false;
            }
        }

        private void ToggleInventory()
        {
            playerMenu.SetActive(!playerMenu.activeSelf);
            playerInventory.ForceUpdate();
        }

        private void ProcessItemSlot(DragAndDrop itemSlot)
        {
            var slot = itemSlot.GetSlotID();
            Inventory inventory;
            Item itemInSlot;
            int amountInSlot;

            if (itemSlot.GetInventoryID() == InventoryID.player)
            {
                inventory = playerInventory;
            }
            else
            {
                inventory = sledInventory;
            }

            itemInSlot = inventory.GetItem(slot);
            amountInSlot = inventory.GetAmount(slot);

            if (itemMouse.GetItem() == null)
            {
                if (itemInSlot != null)
                {
                    itemMouse.SetItem(itemInSlot, amountInSlot);
                    inventory.RemoveItemFromSlot(slot);
                }
            }
            else
            {
                if (inventory.GetItem(slot) == null)
                {
                    inventory.AddItemToSlot(itemMouse.GetItem(), itemMouse.GetAmount(), slot);
                    itemMouse.RemoveItem();
                }
                else if (inventory.GetItem(slot) != itemMouse.GetItem())
                {
                    ItemSlot tempItem = new ItemSlot(inventory.GetItem(slot), inventory.GetAmount(slot));
                    inventory.SetItemInSlot(itemMouse.GetItem(), itemMouse.GetAmount(), slot);
                    itemMouse = tempItem;
                }
                else if (inventory.GetItem(slot).IsStackable())
                {
                    inventory.AddAmountInSlot(itemMouse.GetAmount(), slot);
                }
            }
        }

        private void HideAllMenus()
        {
            playerMenu.SetActive(false);
            sledMenu.SetActive(false);
        }
    }
}
