﻿using UnityEngine;

namespace CodeCabana.Questing
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField] Quest quest = null;
        [SerializeField] string objective = "";

        public void CompleteObjective()
        {
            if(quest != null)
            {
                QuestTracker questTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestTracker>();
                questTracker.CompleteObjective(quest, objective);
            }
        }
    }
}
