using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameSave
{
    public int SaveID;
    public string PlayerName;
    public string PlayerLocation;
    public int CharacterVisual;
    public DateTime FirstSavedTime;
    public DateTime LastSavedTime;
    public Dictionary<int, PokemonEntity> PokemonsInPC;
    public Dictionary<int, PokemonEntity> PokemonsInTeam;
    public Dictionary<int, int> Inventory;
    public int[] UnlockedBadges;
    public Dictionary<int, int> QuestProgression;
}