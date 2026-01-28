using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private List<(int, int)> inventory;
    [SerializeField] private List<(int, int)> currency;
    [SerializeField] private PokemonEntity[] pokemonTeam = new PokemonEntity[6];
    [SerializeField] private List<PokemonEntity> pokemonPC;

    private int currentPokemonInTeam = 0;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pokemonTeam = new PokemonEntity[6];
    }

    public void AddInventory(int itemId, int amount)
    {
        inventory.Add((itemId, amount));
    }
    public void RemoveInventory(int itemId, int amount)
    {
        var correspondence = inventory.Where(i => i.Item1 == itemId).FirstOrDefault();
        if (correspondence == default || correspondence.Item2 < amount) { Debug.LogError($"Cant remove {amount} of {itemId}"); }
        else
        {
            correspondence.Item2 -= amount;
        }
    }

    public void AddCurrency(int currencyId, int amount)
    {
        currency.Add((currencyId, amount));
    }
    public void RemoveCurrency(int currencyId, int amount)
    {
        var correspondence = currency.Where(i => i.Item1 == currencyId).FirstOrDefault();
        if (correspondence == default || correspondence.Item2 < amount) { Debug.LogError($"Cant remove {amount} of {currencyId}"); }
        else
        {
            correspondence.Item2 -= amount;
        }
    }

    public void AddPokemonInTeam(PokemonEntity pokemon, int? slot = null)
    {
        if (slot == null)
        {
            if (currentPokemonInTeam >= 6) { Debug.LogError("Player Team is already full "); }
            else
            {
                pokemonTeam[currentPokemonInTeam] = pokemon;
                currentPokemonInTeam++;
            }
        }
        else
        {
            pokemonTeam[slot.Value] = pokemon;
        }
    }
    public void AddPokemonInPC(PokemonEntity pokemon)
    {
        pokemonPC.Add(pokemon);
    }
    public void RemovePokemonInPC(PokemonEntity pokemon)
    {
        pokemonPC.Remove(pokemon);
    }
}
