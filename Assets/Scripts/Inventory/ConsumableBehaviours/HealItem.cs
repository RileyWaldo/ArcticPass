using UnityEngine;
using CodeCabana.Core;

namespace ArcticPass.Inventories.Consumables
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ArcticPass/Create item/Consumable/Healing Item", order = 0)]
    public class HealItem : ItemConsumable
    {
        [Header("Heal Item")]
        [SerializeField] float amount = 0;
        [SerializeField] bool isPercent = false;

        public override void Use(GameObject user)
        {
            Health health = user.GetComponent<Health>();

            if(isPercent)
            {
                health.Heal((amount / 100f) * health.GetMaxHealth());
            }
            else
            {
                health.Heal(amount);
            }

        }
    }
}
