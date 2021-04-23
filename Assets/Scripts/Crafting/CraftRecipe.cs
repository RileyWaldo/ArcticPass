using UnityEngine;
using ArcticPass.Inventories;

namespace ArcticPass.Crafting
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ArcticPass/Crafting recipe", order = 0)]
    public class CraftRecipe : ScriptableObject
    {
        [SerializeField] CraftIngredient[] ingredients = null;
        [SerializeField] Item product = null;
    }
}
