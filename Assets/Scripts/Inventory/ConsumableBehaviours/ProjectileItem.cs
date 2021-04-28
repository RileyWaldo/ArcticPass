using UnityEngine;

namespace ArcticPass.Inventories.Consumables
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ArcticPass/Create item/Consumable/Projectile", order = 0)]
    public class ProjectileItem : ItemConsumable
    {
        [Header("Projectile")]
        [SerializeField] GameObject projectilePrefab;

        public override void Use(GameObject user)
        {
            Debug.Log("Pew Pew!");
        }
    }
}
