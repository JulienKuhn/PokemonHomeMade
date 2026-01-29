using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BranchObjective", menuName = "Quest/Objective/BranchObjective")]
public class BranchObjective : QuestObjective
{
    [SerializeField] private Condition condition;
    [SerializeField] private string[] parameters;

    [SerializeField] private List<QuestObjective> objectivesIfConditionMet;
    [SerializeField] private List<QuestObjective> objectivesIfConditionNotMet;

    private enum Condition
    {
        CombatWon,
    }

    public override void StartObjective(QuestData relatedQuest)
    {
        Debug.Log("cond " + condition.ToString());
        switch (condition)
        {
            case Condition.CombatWon:

                Debug.Log("Checking");
                Debug.Log(CombatManager.instance.FinishedCombats.Contains((int.Parse(parameters[0]), true)));

                if (CombatManager.instance.FinishedCombats.Contains((int.Parse(parameters[0]), true))){ relatedQuest.InsertObjectivesAtIndex(objectivesIfConditionMet, relatedQuest.currentObjectiveIndex + 1); }
                else { relatedQuest.InsertObjectivesAtIndex(objectivesIfConditionNotMet, relatedQuest.currentObjectiveIndex + 1); }
                    break;
        }

        this.OnObjectiveComplete?.Invoke();
    }


}
