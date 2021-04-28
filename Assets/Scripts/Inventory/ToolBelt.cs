using UnityEngine;
using CodeCabana.Saving;

namespace ArcticPass.Inventories
{
    public class ToolBelt : MonoBehaviour, ISaveable
    {
        [SerializeField] int slotAmount = 5;

        ItemConsumable[] toolSlots;

        private void Awake()
        {
            toolSlots = new ItemConsumable[slotAmount];
        }

        public void Use(int index)
        {
            if(toolSlots[index] != null)
                toolSlots[index].Use(gameObject);
        }

        public object CaptureState()
        {
            return toolSlots;
        }

        public void RestoreState(object state)
        {
            toolSlots = state as ItemConsumable[];
        }
    }
}
