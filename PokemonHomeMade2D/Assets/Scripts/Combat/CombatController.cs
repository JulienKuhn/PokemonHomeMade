using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI; // Si tu as des textes/barres de vie

public enum CombatState { START, PLAYERTURN, ENEMYTURN, BUSY, WON, LOST }

public class CombatController : MonoBehaviour
{
    [SerializeField] private CombatUIController uiController;

    [Header("Participants")]
    public PokemonBase Pokemon;
    public List<PokemonEntity> playerPokemons;
    public List<PokemonEntity> enemyPokemons;

    private PokemonEntity enemyCurrentPokemon, playerCurrentPokemon;

    private int? combatID;
    private CombatState state;

    void Start()
    {
        
    }

    public void StartCombat(List<PokemonEntity> enemypokemons,int? combatID = null)
    {
        this.enemyPokemons = new List<PokemonEntity>();
        foreach (var referencePokemon in enemypokemons)
        {
            PokemonEntity copy = new PokemonEntity(referencePokemon.BaseData, referencePokemon.Level);
            this.enemyPokemons.Add(copy);
        }
        this.combatID = combatID;

        playerPokemons = new List<PokemonEntity>();
        var pk = new PokemonEntity(Pokemon, 2);
        pk.Name = "Player";
        playerPokemons.Add(pk);

        state = CombatState.START;
        StartCoroutine(SetupCombat());
    }

    private IEnumerator SetupCombat()
    {
        playerCurrentPokemon = playerPokemons[0];
        enemyCurrentPokemon = enemyPokemons[0];

        uiController.Log($"Un {enemyCurrentPokemon.Name} sauvage apparaît !");
        yield return new WaitForSeconds(2f);

        uiController.StartCombat(playerCurrentPokemon, ()=>this.DefineNextTurn());
    }

    private void DefineNextTurn()
    {
        if (isGameFinished())
        {
            uiController.Log("FIN");
        }

        // TODO : Check Which pokemon should attack first
        PlayerTurn();
    }


    private void PlayerTurn()
    {
        state = CombatState.PLAYERTURN;
        uiController.Log("Choisissez une attaque !");

        uiController.OnMoveSelected += this.OnAttackButton;
        uiController.StartTurn();

    }

    // Cette fonction est appelée quand tu cliques sur un bouton d'attaque
    public void OnAttackButton(int moveID)
    {
        uiController.OnMoveSelected = null;
        if (state != CombatState.PLAYERTURN) return;

        StartCoroutine(PerformPlayerAttack(moveID));
    }

    private IEnumerator PerformPlayerAttack(int moveID)
    {
        state = CombatState.BUSY;

        // Récupérer l'attaque via ton manager
        MoveBase move = PokemonManager.instance.GetMoveByID(moveID);
        uiController.Log($"{playerCurrentPokemon.Name} utilise {move.moveName} !");

        // Calcul des dégâts
        ApplyDamage(move, playerCurrentPokemon, enemyCurrentPokemon);

        yield return new WaitForSeconds(1f);

        if (enemyCurrentPokemon.IsKO())
        {
            state = CombatState.WON;
            EndBattle();
        }
        else
        {
            state = CombatState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        uiController.Log($"{enemyCurrentPokemon.Name} attaque !");

        // IA Simple : choisit la première attaque apprise
        int randomizedMove = Random.Range(0, enemyCurrentPokemon.BaseData.MovesByLevel.Count - 1);
        int enemyMoveID = enemyCurrentPokemon.BaseData.MovesByLevel[1].moveBaseID;
        MoveBase move = PokemonManager.instance.GetMoveByID(enemyMoveID);

        ApplyDamage(move, enemyCurrentPokemon, playerCurrentPokemon);

        yield return new WaitForSeconds(1f);

        if (playerCurrentPokemon.IsKO())
        {
            state = CombatState.LOST;
            EndBattle();
        }
        else
        {
            state = CombatState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void ApplyDamage(MoveBase moveBase, PokemonEntity attacker, PokemonEntity target)
    {
        Move moveData = moveBase.MainMove;
        float damage = 0;

        int rand = Random.Range(0, 100);
        if(rand <= moveData.accuracyPercentage)
        {
            if (moveData.IsDamaging)
            {
                // Formule simplifiée inspirée du jeu officiel
                float attackStat = (moveData.MoveType == CustomEnums.MoveType.Physical) ? attacker.Attack : attacker.SpAttack;
                float defenseStat = (moveData.MoveType == CustomEnums.MoveType.Physical) ? target.Defense : target.SpDefense;

                //damage = (((2f * attacker.Level / 5f + 2f) * moveData.power * (attackStat / defenseStat)) / 50f) + 2f;
                damage = moveData.power;
            }

            target.ChangeHP(-Mathf.FloorToInt(damage));
            uiController.Log($"{target.Name} reçoit {Mathf.FloorToInt(damage)} dégâts. PV restants : {target.CurrentHP}");
        }
        else
        {
            uiController.Log($"{attacker.Name} rate");
        }
    }

    private void EndBattle()
    {
        if (state == CombatState.WON) uiController.Log("Victoire !");
        else if (state == CombatState.LOST) uiController.Log("Défaite...");

        CombatManager.instance.FinshCombat(combatID, state == CombatState.WON);
        uiController.StopCombat();
    }

    private bool isGameFinished()
    {
        return playerCurrentPokemon.IsKO() || enemyCurrentPokemon.IsKO();
    }
}