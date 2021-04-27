using UnityEngine;
using CodeCabana.Inventories;

namespace ArcticPass.Inventories
{
    public class ToolBelt : MonoBehaviour
    {
        [SerializeField] int slotAmount = 5;

        InventoryItem[] toolSlots;

        private void Awake()
        {
            for(int i=0; i<slotAmount; i++)
            {
                toolSlots[i] = null;
            }
        }


    }
}
