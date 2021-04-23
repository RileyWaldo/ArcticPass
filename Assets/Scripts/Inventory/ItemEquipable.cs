﻿using UnityEngine;

namespace ArcticPass.Inventories
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "ArcticPass/Create item/Equipable", order = 0)]
    public class ItemEquipable : Item
    {
        [Header("Equipment")]
        [SerializeField] int slot = 0;
        [SerializeField] float attackBonus = 0;
        [SerializeField] float defenceBonus = 0;
        [SerializeField] float speedBonus = 0;
    }
}
