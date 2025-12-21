using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public string PlayerName="Evan GigaChad";
    public PokemonEntity[] PokemonTeam = new PokemonEntity[6];

    private void Awake()
    {
        PlayerManager.Instance = this;
    }
}
