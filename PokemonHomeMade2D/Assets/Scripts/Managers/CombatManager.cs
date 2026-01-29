using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    [SerializeField] private CombatController controller;

    public Action<int?,bool> OnCombatFinished;
    public List<(int?, bool)> FinishedCombats = new List<(int?, bool)>();

    private void Awake()
    {
        instance = this;
    }

    public void StartCombat(List<PokemonEntity> enemypokemons, int? id)
    {
        controller.StartCombat(enemypokemons,id);
    }

    public void FinshCombat(int? id, bool isWon)
    {
        FinishedCombats.Add((id, isWon));
        OnCombatFinished?.Invoke(id, isWon);
    }

}
