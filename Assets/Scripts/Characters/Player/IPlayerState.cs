using UnityEngine.InputSystem;

namespace ArcticPass.Character
{
    public interface IPlayerState
    {
        void OnAttack();
        void OnMovement(InputAction.CallbackContext context);
    }
}
