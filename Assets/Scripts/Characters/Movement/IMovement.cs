using UnityEngine;

namespace ArcticPass.CharacterControllers.Movement
{
    public interface IMovement
    {
        void AddForce(Vector2 force);
        void SetVelocity(Vector2 velocity);
    }
}
