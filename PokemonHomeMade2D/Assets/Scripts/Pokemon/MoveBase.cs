using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector; 
using static CustomEnums;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]
public class MoveBase : ScriptableObject
{
    [Header("General Info")]
    public string moveName;
    public int moveID;
    [TextArea] public string description;
    public PokemonType moveType;
    public Sprite moveIcon;

    [Header("Main Move")]
    public Move MainMove;

    [Header("Secondary Effects")]
    public bool hasSecondaryEffect;

    [ShowIf("hasSecondaryEffect")]
    public Move SecondaryMove;

    [Header("Flavor & Flags")]
    public bool contactMove; // Affects "Static" or "Rough Skin"
    public bool soundBased;   // Affects "Soundproof"
    public bool isPunchMove; // Affects "Iron Fist"

}

[System.Serializable]
public class StatusAlteration
{
    public Stat stat;
    public float alterationDuration;
    public TargetType target;
    public StatusCondition statusToInflict;
}

[System.Serializable]
public class Move
{
    public MoveType MoveType;

    // If Physical or Special
    [ShowIf("IsDamagingMove")]
    public Move move;

    // If Status
    [ShowIf("MoveType", MoveType.Status)]
    public StatusAlteration statusAlteration;

    // Common stats
    public int power; // if phy or spe = Amount of dmg flat  | if Status = strenght of the buff/debuff
    [Unit(Units.Percent)]
    public int accuracyPercentage = 100;
    public bool neverMisses;
    public int maxPP;
    public int priority = 0; // Moves with higher priority will be execute first

    [Header("Targeting")]
    public TargetType target;

    [Header("Critical Hit")]
    [Range(0f, 100f)]
    public float flatCritChance = 6.25f; // Standard: 6.25% (1/16)
    public bool IsDamaging => MoveType == MoveType.Physical || MoveType == MoveType.Special;
}