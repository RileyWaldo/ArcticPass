using UnityEngine;

namespace ArcticPass.Inventories.Consumables
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ArcticPass/Create item/Consumable/MythicScroll", order = 0)]
    public class MythicScroll : ItemConsumable
    {
        public override void Use(GameObject user)
        {
            base.Use(user);
        }
    }
}
