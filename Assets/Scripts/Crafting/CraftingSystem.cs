using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcticPass.Crafting
{
    public class CraftingSystem : MonoBehaviour
    {
        [SerializeField] List<CraftRecipe> recipes = new List<CraftRecipe>();

        public event Action onUpdate;

        public bool CanCraft()
        {
            return true;
        }

        public void Craft()
        {
            //put item in inventory.
        }
        
        public void AddRecipe(CraftRecipe recipe)
        {
            recipes.Add(recipe);
            onUpdate?.Invoke();
        }

        public void RemoveRecipe(CraftRecipe recipe)
        {
            recipes.Remove(recipe);
            onUpdate?.Invoke();
        }
    }
}
