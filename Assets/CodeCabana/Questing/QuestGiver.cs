﻿using UnityEngine;

namespace CodeCabana.Questing
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest = default;

        public void GiveQuest()
        {
            QuestTracker questTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestTracker>();
            questTracker.AddQuest(quest);
        }
    }
}
