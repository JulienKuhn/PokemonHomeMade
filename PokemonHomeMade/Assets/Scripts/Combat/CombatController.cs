using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;
using DG.Tweening;
using System;

public class CombatController : MonoBehaviour
{
    public bool isGameOver;
    public bool isTurnOnGoing;
    private bool isVictory;

    // test values
    public PokemonData testpok;
    private PokemonEntity CurrentOpponentPokemon;
    private PokemonEntity CurrentPlayerPokemon;

    private PokemonSpell nextSpell = null;
    [SerializeField] private SelectionMenuController selectionMenuController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        CurrentOpponentPokemon = new PokemonEntity(testpok,5);
        CurrentPlayerPokemon = new PokemonEntity(testpok, 4);

        // Start Combat Opening Animation
        StartCoroutine(OpenCombat());
    }

    private IEnumerator OpenCombat()
    {
        Debug.Log("Opent Combat");
        selectionMenuController.DoDescription("Oh non, un combat de con...");
        yield return new WaitForSeconds(.5f);
        // Start Combat Loop
        StartCoroutine(CombatLoop());
    }

    private IEnumerator CombatLoop()
    {
        Debug.Log("CombatLoop");
        yield return null;
        isGameOver = false;

        while (!isGameOver)
        {

            // NEW TURN ! 
            // Calculate who start the turn
            if (isPlayerStartingTurn())
            {
                StartCoroutine(DoPlayerTurn());
                yield return new WaitWhile(() => isTurnOnGoing);
                StartCoroutine(DoComputerTurn());
                yield return new WaitWhile(() => isTurnOnGoing);
            }
            else
            {
                StartCoroutine(DoComputerTurn());
                yield return new WaitWhile(() => isTurnOnGoing);
                StartCoroutine(DoPlayerTurn());
                yield return new WaitWhile(() => isTurnOnGoing);
            }

            // Check is GameIsOver
            isGameOver = CheckIfGameIsOver();
        }

        if (isVictory) 
        {
            // Do Victory Screen
        }
        else
        {
            // Do GameOver Screen
            // Teleport Player to Latest medical center
            // Start respawn dialogue
        }
    }

    private IEnumerator DoPlayerTurn()
    {
        Debug.Log("DoPlayerTurn");
        isTurnOnGoing = true;
        yield return null;
        selectionMenuController.OpenChoices(CurrentPlayerPokemon.Spells, $"Que doit faire {CurrentPlayerPokemon.Name} ?");
        nextSpell = null;
        selectionMenuController.OnSpellSelected += OnActionRecieved;
        yield return new WaitWhile(()=> nextSpell == null);

        selectionMenuController.DoDescription($"{CurrentPlayerPokemon.Name} lance {nextSpell.name} !");
        yield return new WaitForSeconds(1f);
        yield return null;
        isTurnOnGoing = false;
    }

    private void OnActionRecieved(PokemonSpell spell)
    {
        selectionMenuController.OnSpellSelected = null;
        nextSpell = spell;
    }

    private IEnumerator DoComputerTurn()
    {
        Debug.Log("DoComputerTurn");
        isTurnOnGoing = true;
        yield return null;

        selectionMenuController.DoDescription($"Au tour de {CurrentOpponentPokemon.Name}");
        yield return new WaitForSeconds(.5f);

        yield return null;
        isTurnOnGoing = false;
    }

    public bool isPlayerStartingTurn()
    {
        if(CurrentPlayerPokemon.Speed == CurrentOpponentPokemon.Speed)
        {
            int rand = UnityEngine.Random.Range(0, 100);
            return rand > 50;
        }

        return CurrentPlayerPokemon.Speed >= CurrentOpponentPokemon.Speed;
    }

    public bool CheckIfGameIsOver()
    {
        return isGameOver;
    }
}
