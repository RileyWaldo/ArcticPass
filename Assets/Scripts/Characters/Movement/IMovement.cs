using UnityEngine;
using ArcticPass.CharacterControllers.Actions;

namespace ArcticPass.CharacterControllers.Movement
{
    public interface IMovement : IAction
    {
        void AddForce(Vector2 force);
        void SetVelocity(Vector2 velocity);
    }
}
