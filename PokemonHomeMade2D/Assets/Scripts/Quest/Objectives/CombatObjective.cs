using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatObjective", menuName = "Quest/Objective/CombatObjective")]
public class CombatObjective : QuestObjective
{
    [SerializeField] private int CombatID;
    [SerializeField] private bool MustBeAWin;

    [SerializeField] private List<PokemonEntity> pokemons;

    public override void StartObjective(QuestData relatedQuest)
    {
        CombatManager.instance.OnCombatFinished += this.OnCombatFinished;
        CombatManager.instance.StartCombat(pokemons, CombatID);
    }

    private void OnCombatFinished(int? id, bool isWon)
    {
        if(id != CombatID) return;
        if (MustBeAWin && !isWon) return;

        this.OnObjectiveComplete?.Invoke();
    }
}
