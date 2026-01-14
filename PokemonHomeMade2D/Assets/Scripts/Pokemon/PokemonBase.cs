using System.Collections.Generic;
using UnityEngine;
using static CustomEnums;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class PokemonBase : ScriptableObject
{
    [Header("General Info")]
    public int PokemonID;
    public string PokemonName;
    [TextArea] public string Description;
    public List<PokemonType> PokemonTypes;

    [Header("Visuals")]
    public Sprite FrontVisuals;
    public Sprite BackVisuals;
    public Sprite SplashVisual;
    public Sprite WorldVisuals;

    [Header("Evolution")]
    public int NextEvolutionID; // Référence directe au prochain SO
    public int LevelRequiredBeforeEvol;

    [Header("Learned Spells")]
    public List<LearnableMove> MovesByLevel; // Utilise une liste pour l'inspecteur

    [Header("Combat Stats")]
    public int BaseHP;
    public int BaseAttack;
    public int BaseDefense;
    public int BaseAttackSPE;
    public int BaseDefenseSPE;
    public int BaseSpeed;


    [Header("Basic Stats")]
    public float MalePercentage;
    public float Height;
    public float Weight;
    public int CaptureRatio;
}

[System.Serializable]
public class LearnableMove
{
    public MoveBase moveBase;
    public int level;
}

[System.Serializable]
public class StatValues
{
    public int hp, attack, defense, speed, special;
}