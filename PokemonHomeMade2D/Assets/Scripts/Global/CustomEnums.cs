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
        Steel,
        Dark,
        Fairy,
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
    public enum MoveType
    {
        Physical,
        Special,
        Status
    }
    public enum TargetType { Foe, Self, AllFoes, AllAllies }
    public enum StatusCondition { None, Burn, Sleep, Poison, Paralyze, Freeze, Confused }
    public enum Stat { Attack, Defense, SpecialAttack, SpecialDefense, Speed, Accuracy, Evasion }
}
