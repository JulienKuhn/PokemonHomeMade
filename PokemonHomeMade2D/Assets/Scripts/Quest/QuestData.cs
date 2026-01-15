using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Quest/Create new Quest")]
public class QuestData : ScriptableObject
{
    [SerializeField] private List<QuestObjective> objectives;
    [SerializeField] private int currentObjectiveIndex = 0;

    QuestObjective currentObjective;

    public void StartQuest(int resumeTo = 0)
    {
        currentObjectiveIndex = resumeTo;
        currentObjective = objectives[currentObjectiveIndex];
        currentObjective.StartObjective();
    }
}
