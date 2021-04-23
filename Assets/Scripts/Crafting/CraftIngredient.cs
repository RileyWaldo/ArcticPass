using ArcticPass.Inventories;

namespace ArcticPass.Crafting
{
    [System.Serializable]
    public class CraftIngredient
    {
        public Item item = null;
        public int amount = 1;
        public bool dispose = true;
    }
}
