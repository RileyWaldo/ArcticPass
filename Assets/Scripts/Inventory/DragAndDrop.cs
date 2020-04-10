using UnityEngine;
using UnityEngine.UI;

namespace ArcticPass.UI
{
    public class DragAndDrop : MonoBehaviour
    {
        [SerializeField] Image image = null;
        [SerializeField] Text text;

        InventoryID inventoryID;
        int slotID = 0;

        private void Start()
        {
            SetSprite(null, 0);
        }

        public void SetUp(InventoryID id, int slot)
        {
            inventoryID = id;
            slotID = slot;
        }

        public void SetSprite(Sprite sprite, int amount)
        {
            if (sprite == null)
            {
                image.gameObject.SetActive(false);
                text.text = "";
            }
            else
            {
                image.gameObject.SetActive(true);
                image.sprite = sprite;
                if (amount > 1)
                    text.text = amount.ToString();
                else
                    text.text = "";
            }
        }

        public int GetSlotID()
        {
            return slotID;
        }

        public InventoryID GetInventoryID()
        {
            return inventoryID;
        }
    }
}
