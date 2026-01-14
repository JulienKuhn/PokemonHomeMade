using UnityEngine;

public static class CustomEnums
{
    public enum PokemonType
    {
        None, 
        Normal, 
        Fire, 
        Water, 
        Grass, 
        Electric, 
        Ice, 
        Fighting,
        Poison, 
        Ground, 
        Flying, 
        Psychic, 
        Bug, 
        Rock, 
        Ghost, 
        Dragon,
    }
    public enum PokemonNature
    {
        Hardy,
        Adamant,
        Modest,
        Jolly,
        Timid,
        Careful,
    }
    public enum SpellCategory
    {
        Physical, // Uses Attack vs Defense
        Special,  // Uses SpAttack vs SpDefense
        Status    // No damage, applies buffs/debuffs or status conditions
    }
}
