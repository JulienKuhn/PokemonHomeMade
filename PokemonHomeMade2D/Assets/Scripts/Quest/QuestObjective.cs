using System;
using UnityEngine;

public class QuestObjective : ScriptableObject
{
    public Action OnObjectiveComplete;

    public virtual void StartObjective(QuestData relatedQuest)
    {
    }
}
