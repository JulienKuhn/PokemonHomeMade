using System.Collections.Generic;
using UnityEngine;
using static CustomEnums;

[System.Serializable]
public class PokemonEntity
{
    // Reference to the base "Blueprint" of the species
    [SerializeField] private PokemonBase pokemonBase;
    [SerializeField] private int level;

    public string Name;
    // Individual Values (IVs) - Randomly generated from 0 to 31
    public int IV_HP;
    public int IV_Attack;
    public int IV_Defense;
    public int IV_SpAttack;
    public int IV_SpDefense;
    public int IV_Speed;

    // Dynamic Stats (Calculated based on Level, Base Stats, and IVs)
    public int MaxHP;
    public int CurrentHP;
    public int Attack;
    public int Defense;
    public int SpAttack;
    public int SpDefense;
    public int Speed;

    public PokemonNature Nature;

    public List<(MoveBase, int)> LearnedMoves;
    public float CurrentExp;
    public enum StatType { Attack, Defense, SpAttack, SpDefense, Speed }

    public PokemonBase BaseData => pokemonBase;
    public int Level => level;

    /// <summary>
    /// Constructor to create a new Pokemon instance.
    /// </summary>
    public PokemonEntity(PokemonBase data, int level)
    {
        this.pokemonBase = data;
        this.level = level;
        this.Name = data.PokemonName;

        GenerateRandomIVs();
        GenerateRandomNature();
        CalculateStats();

        // Start at full health
        CurrentHP = MaxHP;

        LearnedMoves = new List<(MoveBase, int)>();
        foreach (var move in data.MovesByLevel)
        {
            if (level >= move.level)
            {
                MoveBase moveBase = PokemonManager.instance.GetMoveByID(move.moveBaseID);
                LearnedMoves.Add((moveBase, moveBase.MainMove.maxPP));
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
        MaxHP = Mathf.FloorToInt(((pokemonBase.BaseHP + IV_HP) * 2 * level) / 100f) + level + 10;

        // Regular stats formula
        Attack = CalculateBaseStat(pokemonBase.BaseAttack, IV_Attack, StatType.Attack);
        Defense = CalculateBaseStat(pokemonBase.BaseDefense, IV_Defense, StatType.Defense);
        SpAttack = CalculateBaseStat(pokemonBase.BaseAttackSPE, IV_SpAttack, StatType.SpAttack);
        SpDefense = CalculateBaseStat(pokemonBase.BaseDefenseSPE, IV_SpDefense, StatType.SpDefense);
        Speed = CalculateBaseStat(pokemonBase.BaseSpeed, IV_Speed, StatType.Speed);
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
}