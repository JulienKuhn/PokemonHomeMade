using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "QuestData", menuName = "Quest/Create new Quest")]
public class QuestData : ScriptableObject
{
    [SerializeField] private List<QuestObjective> objectives;
    [SerializeField] private int currentObjectiveIndex = 0;

    QuestObjective currentObjective;

    public void StartQuest(int resumeTo = 0)
    {
        currentObjectiveIndex = resumeTo;
        StartNewObjective();
    }

    private void OnObjectiveComplete()
    {
        currentObjective.OnObjectiveComplete = null;
        currentObjectiveIndex++;
        if (currentObjectiveIndex >= objectives.Count)
        {
            Debug.Log("Quest in finished");
        }
        else
        {
            StartNewObjective();
        }
    }

    private void StartNewObjective()
    {
        currentObjective = objectives[currentObjectiveIndex];
        currentObjective.OnObjectiveComplete += this.OnObjectiveComplete;
        currentObjective.StartObjective();
    }
}
