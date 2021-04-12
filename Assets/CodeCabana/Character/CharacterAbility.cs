using UnityEngine;

namespace CodeCabana.CharacterControllers
{
    [RequireComponent(typeof(Character))]
    public abstract class CharacterAbility : MonoBehaviour
    {
        public bool IsActive { get; set; } = true;

        public abstract void OnUpdateAbility();
    }
}
