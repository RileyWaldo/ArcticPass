using UnityEngine;
using ArcticPass.Input;

namespace ArcticPass.CharacterControllers.Player
{
    public class PlayerController : MonoBehaviour
    {
        Character character;
        InputMaster input;

        private void Awake()
        {
            SetUpInput();

            character = GetComponent<Character>();
        }

        //Input System methods

        private void SetUpInput()
        {
            input = new InputMaster();
            input.Player.Movement.performed += context => Move(context.ReadValue<Vector2>());
            input.Player.Movement.canceled += context => MoveEnd();
            input.Player.Attack.performed += context => Attack();
        }

        private void Attack()
        {
            Debug.Log("Attack!");
        }

        private void Move(Vector2 direction)
        {
            character.Animator.speed = 1f;
            character.Animator.SetFloat("Horizontal", direction.x);
            character.Animator.SetFloat("Vertical", direction.y);
            character.Movement.SetVelocity(direction * 5f);
        }

        private void MoveEnd()
        {
            character.Animator.speed = 0;
            character.Movement.SetVelocity(Vector2.zero);
        }

        private void OnEnable() => input.Player.Enable();

        private void OnDisable() => input.Player.Disable();
    }
}
