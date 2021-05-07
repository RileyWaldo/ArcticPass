using UnityEngine;
using UnityEngine.Assertions;
using ArcticPass.Input;
using ArcticPass.CharacterControllers.Actions;

namespace ArcticPass.CharacterControllers.Player
{
    public class PlayerController : MonoBehaviour
    {
        Character character;
        ActionScheduler actionScheduler;
        InputMaster input;

        private void Awake()
        {
            SetUpInput();

            character = GetComponent<Character>();
            actionScheduler = GetComponent<ActionScheduler>();

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
            character.Animator.speed = 1f;
            character.Animator.SetBool("attack", true);
            actionScheduler.StartAction(character.Attack, false);
        }

        private void Move(Vector2 direction)
        {
            if (!actionScheduler.IsCurrentActionFinished())
                return;

            character.Animator.speed = 1f;
            character.Animator.SetFloat("Horizontal", direction.x);
            character.Animator.SetFloat("Vertical", direction.y);
            character.Movement.SetVelocity(direction * 5f);
            actionScheduler.StartAction(character.Movement, true);
        }

        private void MoveEnd()
        {
            if (!actionScheduler.IsCurrentActionFinished())
                return;

            character.Animator.speed = 0;
            character.Movement.SetVelocity(Vector2.zero);
        }

        private void AssertSetUp()
        {
            Assert.IsNotNull(character, "Player Controller requires Character component.");
            Assert.IsNotNull(actionScheduler, "Player Controller requires Action Scheduler component.");
        }

        private void OnEnable() => input.Player.Enable();

        private void OnDisable() => input.Player.Disable();
    }
}
