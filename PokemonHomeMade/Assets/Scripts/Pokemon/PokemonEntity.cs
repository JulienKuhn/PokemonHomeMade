using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents an individual Pokémon instance in the game (e.g., in your party or a wild encounter).
/// It uses data from a PokemonData ScriptableObject and applies unique modifiers like Level, IVs, and Nature.
/// </summary>
[System.Serializable]
public class PokemonEntity
{
    // Reference to the base "Blueprint" of the species
    [SerializeField] private PokemonData baseData;
    [SerializeField] private int level;

    public string Name;
    // Individual Values (IVs) - Randomly generated from 0 to 31
    public int IV_HP { get; private set; }
    public int IV_Attack { get; private set; }
    public int IV_Defense { get; private set; }
    public int IV_SpAttack { get; private set; }
    public int IV_SpDefense { get; private set; }
    public int IV_Speed { get; private set; }

    // Dynamic Stats (Calculated based on Level, Base Stats, and IVs)
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int SpAttack { get; private set; }
    public int SpDefense { get; private set; }
    public int Speed { get; private set; }

    public PokemonNature Nature { get; private set; }

    public Dictionary<PokemonSpell,int> LearnedSpells { get; private set; }
    public float CurrentExp { get; private set; }

    /// <summary>
    /// Constructor to create a new Pokemon instance.
    /// </summary>
    public PokemonEntity(PokemonData data, int level)
    {
        this.baseData = data;
        this.level = level;
        this.Name = data.PokemonName;

        GenerateRandomIVs();
        GenerateRandomNature();
        CalculateStats();

        // Start at full health
        CurrentHP = MaxHP;


        LearnedSpells = new Dictionary<PokemonSpell, int>();
        foreach (var spell in data.SpellsByLevel)
        {
            if(level >= spell.level)
            {
                LearnedSpells.Add(spell.spell, spell.spell.maxPP);
            }
        }
    }

    /// <summary>
    /// Generates unique hidden values for this specific instance.
    /// </summary>
    private void GenerateRandomIVs()
    {
        IV_HP = UnityEngine.Random.Range(0, 32);
        IV_Attack = UnityEngine.Random.Range(0, 32);
        IV_Defense = UnityEngine.Random.Range(0, 32);
        IV_SpAttack = UnityEngine.Random.Range(0, 32);
        IV_SpDefense = UnityEngine.Random.Range(0, 32);
        IV_Speed = UnityEngine.Random.Range(0, 32);
    }

    private void GenerateRandomNature()
    {
        // Randomly pick a nature from the Enum
        System.Array values = System.Enum.GetValues(typeof(PokemonNature));
        Nature = (PokemonNature)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    /// <summary>
    /// Calculates the final stats using the standard Pokemon formulas.
    /// Formula: Floor(((Base + IV) * 2 * Level) / 100) + Modifier
    /// </summary>
    public void CalculateStats()
    {
        // HP has a specific formula
        MaxHP = Mathf.FloorToInt(((baseData.BaseHP + IV_HP) * 2 * level) / 100f) + level + 10;

        // Regular stats formula
        Attack = CalculateBaseStat(baseData.BaseAttack, IV_Attack, StatType.Attack);
        Defense = CalculateBaseStat(baseData.BaseDefense, IV_Defense, StatType.Defense);
        SpAttack = CalculateBaseStat(baseData.BaseAttackSPE, IV_SpAttack, StatType.SpAttack);
        SpDefense = CalculateBaseStat(baseData.BaseDefenseSPE, IV_SpDefense, StatType.SpDefense);
        Speed = CalculateBaseStat(baseData.BaseSpeed, IV_Speed, StatType.Speed);
    }

    private int CalculateBaseStat(int baseVal, int ivVal, StatType type)
    {
        float stat = Mathf.FloorToInt(((baseVal + ivVal) * 2 * level) / 100f) + 5;

        // Apply Nature Multiplier (0.9, 1.0, or 1.1)
        return Mathf.FloorToInt(stat * GetNatureMultiplier(Nature, type));
    }

    /// <summary>
    /// Updates the Current HP. Use negative values for damage and positive for healing.
    /// </summary>
    public void ChangeHP(int amount)
    {
        CurrentHP = Mathf.Clamp(CurrentHP + amount, 0, MaxHP);
    }

    public bool IsKO() => CurrentHP <= 0;

    // --- Helper Methods & Enums ---

    private float GetNatureMultiplier(PokemonNature nature, StatType stat)
    {
        // Simple example: Adamant (+Atk, -SpAtk)
        if (nature == PokemonNature.Adamant)
        {
            if (stat == StatType.Attack) return 1.1f;
            if (stat == StatType.SpAttack) return 0.9f;
        }
        // Modest (+SpAtk, -Atk)
        if (nature == PokemonNature.Modest)
        {
            if (stat == StatType.SpAttack) return 1.1f;
            if (stat == StatType.Attack) return 0.9f;
        }

        return 1.0f; // Neutral multiplier
    }

    public enum StatType { Attack, Defense, SpAttack, SpDefense, Speed }

    public PokemonData BaseData => baseData;
    public int Level => level;
}
