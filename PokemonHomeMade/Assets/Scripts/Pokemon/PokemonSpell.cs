using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "PokemonSpell", menuName = "Scriptable Objects/PokemonSpell")]
public class PokemonSpell : ScriptableObject
{
    public string Name;
    public int SpellID;
    public float FlatCritChance;
    public int PowerPointCost;
    public Sprite Visuals;
}
