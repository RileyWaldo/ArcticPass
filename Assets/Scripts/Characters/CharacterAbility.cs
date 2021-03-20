using UnityEngine;

namespace ArcticPass.Character
{
    [RequireComponent(typeof(Character))]
    public abstract class CharacterAbility : MonoBehaviour
    {
        protected void Awake()
        {
            GetComponent<Character>().AddAbility(this);
        }

        public bool IsActive { get; set; } = true;

        public abstract void OnUpdateAbility();
    }
}
