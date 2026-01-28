using UnityEngine;
using static CustomEnums;

public static class PokemonStringifier
{
    public static string GetTypeAcronym(PokemonType type)
    {
        switch (type)
        {
            case PokemonType.Normal: return "NRM";
            case PokemonType.Fire: return "FEU";
            case PokemonType.Water: return "EAU";
            case PokemonType.Grass: return "PLT";
            case PokemonType.Electric: return "ELE";
            case PokemonType.Ice: return "GLA";
            case PokemonType.Fighting: return "CMB"; 
            case PokemonType.Poison: return "PSN";
            case PokemonType.Ground: return "SOL";
            case PokemonType.Flying: return "VOL";
            case PokemonType.Psychic: return "PSY";
            case PokemonType.Bug: return "INS"; 
            case PokemonType.Rock: return "ROC";
            case PokemonType.Ghost: return "SPE"; 
            case PokemonType.Dragon: return "DRA";
            case PokemonType.Steel: return "ACI"; 
            case PokemonType.Dark: return "TNB";
            case PokemonType.Fairy: return "FEE";
            case PokemonType.None: return "---";
            default: return "---";
        }
    }
}
