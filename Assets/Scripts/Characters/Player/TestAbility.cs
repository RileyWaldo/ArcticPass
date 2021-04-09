using UnityEngine;
using CodeCabana.CharacterControllers;

namespace ArcticPass.CharacterControllers.Player
{
    public class TestAbility : CharacterAbility
    {
        float time = 0;

        public override void OnUpdateAbility()
        {
            Debug.Log("Testing ability 1");
            time += Time.deltaTime;
            if(time > 3f)
            {
                IsActive = false;
                Debug.Log("Deactivated ability 1");
            }
        }
    }
}
