using UnityEngine;

namespace ArcticPass.CharacterControllers.Actions
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public bool StartAction(IAction action)
        {
            if (currentAction == action)
                return false;

            if (currentAction != null)
            {
                if (!currentAction.CanOverride())
                    return false;
                currentAction.Cancel();
            }

            currentAction = action;
            return true;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
