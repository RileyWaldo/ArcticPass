using UnityEngine;

namespace ArcticPass.CharacterControllers.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementPhysics : MonoBehaviour, IMovement
    {
        Rigidbody2D rigidBody;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        public void AddForce(Vector2 force)
        {
            rigidBody.AddForce(force);
        }

        public void SetVelocity(Vector2 velocity)
        {
            rigidBody.velocity = velocity;
        }
    }
}
