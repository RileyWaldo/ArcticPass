using UnityEngine;
using UnityEngine.Assertions;
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

            AssertSetUp();
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
            character.Attack();
        }

        private void Move(Vector2 direction)
        {
            character.Move(direction);
        }

        private void MoveEnd()
        {
            character.MoveEnd();
        }

        private void AssertSetUp()
        {
            Assert.IsNotNull(character, "Player Controller requires Character component.");
        }

        private void OnEnable() => input.Player.Enable();

        private void OnDisable() => input.Player.Disable();
    }
}
