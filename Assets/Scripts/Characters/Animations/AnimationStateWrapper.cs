using UnityEngine;

namespace ArcticPass.CharacterControllers.Animations
{
    public abstract class AnimationStateWrapper<T> : StateMachineBehaviour
        where T : MonoBehaviour
    {
        T monoBehaviour;

        public static void Initialize(AnimationStateWrapper<T> animationWrapper, T monoBehaviour)
        {
            animationWrapper.monoBehaviour = monoBehaviour;
        }

        public virtual void OnAnimationBegin(T monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public virtual void OnAnimationEnd(T monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public virtual void OnAnimationUpdate(T monoBehaviour, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnAnimationBegin(monoBehaviour, animator, stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnAnimationEnd(monoBehaviour, animator, stateInfo);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnAnimationUpdate(monoBehaviour, animator, stateInfo);
        }
    }
}
