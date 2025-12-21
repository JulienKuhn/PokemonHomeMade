using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPokemon", menuName = "Scriptable Objects/Pokemon")]
public class PokemonData : ScriptableObject // Renommé pour plus de clarté
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
    public PokemonData NextEvolution; // Référence directe au prochain SO
    public int LevelRequiredBeforeEvol;

    [Header("Learned Spells")]
    public List<LearnableSpell> SpellsByLevel; // Utilise une liste pour l'inspecteur

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
public struct LearnableSpell
{
    public int level;
    public PokemonSpell spell;
}

public enum PokemonType
{
    Acier,
    Combat,
    Dragon,
    Eau,
    Electrik,
    Fee,
    Feu,
    Glace,
    Insecte,
    Normal,
    Plante,
    Poison,
    Psy,
    Roche,
    Sol,
    Spectre,
    Tenebres,
    Vol,
    Inconnu,
}
public enum PokemonNature 
{ 
    Hardy, 
    Adamant, 
    Modest, 
    Jolly, 
    Timid, 
    Careful 
}