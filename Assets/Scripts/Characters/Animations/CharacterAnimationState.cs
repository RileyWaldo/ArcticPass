using UnityEngine;

namespace ArcticPass.CharacterControllers.Animations
{
    public class CharacterAnimationState : AnimationStateWrapper<Character>
    {
        public override void OnAnimationEnd(Character character, Animator animator, AnimatorStateInfo stateInfo)
        {
            character.OnAnimationEnd(stateInfo);
        }
    }
}
