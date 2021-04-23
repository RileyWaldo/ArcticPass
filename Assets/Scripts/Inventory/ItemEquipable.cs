using UnityEngine;
using CodeCabana.Inventories;

namespace ArcticPass.Inventories
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "ArcticPass/Create item/Equipable", order = 0)]
    public class ItemEquipable : InventoryItem
    {
        [Header("Equipment")]
        [SerializeField] EquipmentSlot slot = EquipmentSlot.Weapon;
        [SerializeField] float attackBonus = 0;
        [SerializeField] float defenceBonus = 0;
        [SerializeField] float speedBonus = 0;
    }
}
