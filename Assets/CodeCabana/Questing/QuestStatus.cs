﻿using System.Collections.Generic;
using UnityEngine;

namespace CodeCabana.Questing
{
    [System.Serializable]
    public class QuestStatus
    {
        Quest quest;
        List<string> completedObjectives = new List<string>();

        public QuestStatus(object restoreState)
        {
            QuestStatusRecord record = restoreState as QuestStatusRecord;
            quest = Quest.GetByName(record.questName);
            completedObjectives = record.completedObjectives;
        }

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompleteObjectives()
        {
            return completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            if(!quest.HasObjective(objective))
            {
                Debug.Log("IsObjectiveComplete in Quest: " + quest.GetTitle() + " doesn't have objective: " + objective);
                return false;
            }
            foreach(string objectiveToCheck in completedObjectives)
            {
                if (objective == objectiveToCheck)
                    return true;
            }
            return false;
        }

        public void CompleteObjective(string objective)
        {
            if(quest.HasObjective(objective))
            {
                if (!completedObjectives.Contains(objective))
                    completedObjectives.Add(objective);
                else
                    Debug.Log("Already completed objective: " + objective + "in Quest: " + quest.GetTitle());
            }
            else
            {
                Debug.Log("Quest: " + quest.GetTitle() + " does not have objective: " + objective);
            }
        }

        public bool IsComplete()
        {
            foreach(var objective in quest.GetObjectives())
            {
                if (!completedObjectives.Contains(objective.reference))
                    return false;
            }
            return true;
        }

        public object CaptureState()
        {
            QuestStatusRecord record = new QuestStatusRecord();
            record.questName = quest.GetTitle();
            record.completedObjectives = completedObjectives;
            return record;
        }

        [System.Serializable]
        class QuestStatusRecord
        {
            public string questName;
            public List<string> completedObjectives;
        }
    }
}
