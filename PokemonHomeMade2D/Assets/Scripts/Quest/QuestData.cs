using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "QuestData", menuName = "Quest/Create new Quest")]
public class QuestData : ScriptableObject
{
    [SerializeField] public List<QuestObjective> objectives;
    [SerializeField] public int currentObjectiveIndex = 0;

    QuestObjective currentObjective;
    private List<QuestObjective> WorkingObjectives;
    public void StartQuest(int resumeTo = 0)
    {
        this.WorkingObjectives = new List<QuestObjective>();
        foreach (var refObjective in objectives)
        {
            QuestObjective copy = Instantiate(refObjective);
            this.WorkingObjectives.Add(copy);
        }

        currentObjectiveIndex = resumeTo;
        StartNewObjective();
    }

    private void OnObjectiveComplete()
    {
        currentObjective.OnObjectiveComplete = null;
        currentObjectiveIndex++;
        if (currentObjectiveIndex >= WorkingObjectives.Count)
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
        currentObjective = WorkingObjectives[currentObjectiveIndex];
        currentObjective.OnObjectiveComplete += this.OnObjectiveComplete;
        currentObjective.StartObjective(this);
    }

    public void InsertObjectivesAtIndex(List<QuestObjective> objs, int index)
    {
        WorkingObjectives.InsertRange(index, objs);
    }
}
