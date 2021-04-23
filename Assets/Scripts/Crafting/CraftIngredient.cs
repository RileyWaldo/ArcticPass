using CodeCabana.Inventories;

namespace ArcticPass.Crafting
{
    [System.Serializable]
    public class CraftIngredient
    {
        public InventoryItem item = null;
        public int amount = 1;
        public bool dispose = true;
    }
}
