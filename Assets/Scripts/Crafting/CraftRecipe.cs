using UnityEngine;
using CodeCabana.Inventories;

namespace ArcticPass.Crafting
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ArcticPass/Crafting recipe", order = 0)]
    public class CraftRecipe : ScriptableObject
    {
        [SerializeField] CraftIngredient[] ingredients = null;
        [SerializeField] InventoryItem product = null;
    }
}
