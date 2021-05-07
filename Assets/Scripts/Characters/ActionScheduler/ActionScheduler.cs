﻿using UnityEngine;

namespace ArcticPass.CharacterControllers.Actions
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        bool canOverride = true;

        public void StartAction(IAction action, bool canOverride)
        {
            if (!this.canOverride)
                return;
            if (currentAction == action)
                return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            this.canOverride = canOverride;
            currentAction = action;
        }

        public bool IsCurrentActionFinished()
        {
            return !canOverride;
        }

        public void CancelCurrentAction()
        {
            canOverride = true;
            StartAction(null, true);
        }
    }
}
