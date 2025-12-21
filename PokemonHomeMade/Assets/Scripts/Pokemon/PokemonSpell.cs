using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Scriptable Objects/Pokemon Spell")]
public class PokemonSpell : ScriptableObject
{
    [Header("General Info")]
    public string moveName;
    public int moveID;
    [TextArea] public string description;
    public PokemonType moveType; // Elemental type (Fire, Water, etc.)
    public Sprite moveIcon;

    [Header("Combat Stats")]
    public SpellCategory category; // Physical, Special, or Status
    public int power;             // Base damage (e.g., 40 for Tackle, 90 for Thunderbolt)
    public int accuracy = 100;    // Percentage (0 to 100)
    public int maxPP;             // Maximum uses
    public bool isSelfCasted;

    [Header("Critical Hit")]
    [Range(0f, 100f)]
    public float flatCritChance = 6.25f; // Standard Pokémon crit rate is 6.25% (1/16)

    [Header("Effects")]
    public bool hasSecondaryEffect;
    public int effectChance;      // Chance to trigger a status effect (e.g., 10% chance to burn)
}

public enum SpellCategory
{
    Physical, // Uses Attack vs Defense
    Special,  // Uses SpAttack vs SpDefense
    Status    // No damage, applies buffs/debuffs or status conditions
}