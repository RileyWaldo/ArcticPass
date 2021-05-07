using UnityEngine;

namespace ArcticPass.CharacterControllers.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementPhysics : MonoBehaviour, IMovement
    {
        Character character;
        Rigidbody2D rigidBody;

        Vector2 direction = Vector2.zero;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            character = GetComponent<Character>();
        }

        public void AddForce(Vector2 force)
        {
            rigidBody.AddForce(force);
        }

        public void SetVelocity(Vector2 velocity)
        {
            rigidBody.velocity = velocity;
        }

        public void Cancel()
        {
            SetVelocity(Vector2.zero);
        }
    }
}
