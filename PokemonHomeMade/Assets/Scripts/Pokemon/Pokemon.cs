using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Scriptable Objects/Pokemon")]
public class Pokemon : ScriptableObject
{
    public string Name;
    public int PokemonID;
    public int Level;
    public Sprite FrontVisuals, BackVisuals, SplashVisual, WorldVisuals;
    public Dictionary<int,PokemonSpell> SpellsByLevel; // Spells that will be learned at a specific Level
    public Dictionary<int, Pokemon> EvolutionAtLevel; // if not null = next pokemon after reaching a specific level | If null = no Evolution for this pokemon
}
