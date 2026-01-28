using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonManager : MonoBehaviour
{
    public static PokemonManager instance;

    [SerializeField] private List<MoveBase> Moves;
    [SerializeField] private List<PokemonBase> Pokemons;

    private void Awake()
    {
        instance = this;
    }

    public MoveBase GetMoveByID(int id)
    {
        return Moves.Where(m => m.moveID == id).FirstOrDefault();
    }

    public PokemonBase GetPokemonByID(int id)
    {
        return Pokemons.Where(p=>p.PokemonID == id).FirstOrDefault();
    }

}
