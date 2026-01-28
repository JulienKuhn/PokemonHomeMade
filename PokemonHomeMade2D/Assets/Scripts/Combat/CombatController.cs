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
    public PokemonEntity playerPokemon;
    public PokemonEntity enemyPokemon;

    [Header("UI Reference (Optionnel)")]
    // public BattleHUD playerHUD;
    // public BattleHUD enemyHUD;

    private CombatState state;

    void Start()
    {
        state = CombatState.START;
        StartCoroutine(SetupCombat());

        playerPokemon = new PokemonEntity(Pokemon, 2);
        playerPokemon.Name = "Player";
        enemyPokemon = new PokemonEntity(Pokemon, 2);
        enemyPokemon.Name = "Enemy";
    }

    private IEnumerator SetupCombat()
    {
        // Ici, tu pourrais instancier les visuels avec enemyPokemon.BaseData.FrontVisuals
        uiController.Log($"Un {enemyPokemon.Name} sauvage apparaît !");
        yield return new WaitForSeconds(2f);

        uiController.StartCombat(playerPokemon, ()=>this.DefineNextTurn());
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
        uiController.Log($"{playerPokemon.Name} utilise {move.moveName} !");

        // Calcul des dégâts
        ApplyDamage(move, playerPokemon, enemyPokemon);

        yield return new WaitForSeconds(1f);

        if (enemyPokemon.IsKO())
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
        uiController.Log($"{enemyPokemon.Name} attaque !");

        // IA Simple : choisit la première attaque apprise
        int randomizedMove = Random.Range(0, enemyPokemon.BaseData.MovesByLevel.Count - 1);
        int enemyMoveID = enemyPokemon.BaseData.MovesByLevel[randomizedMove].moveBaseID;
        MoveBase move = PokemonManager.instance.GetMoveByID(enemyMoveID);

        ApplyDamage(move, enemyPokemon, playerPokemon);

        yield return new WaitForSeconds(1f);

        if (playerPokemon.IsKO())
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

        if (moveData.IsDamaging)
        {
            // Formule simplifiée inspirée du jeu officiel
            float attackStat = (moveData.MoveType == CustomEnums.MoveType.Physical) ? attacker.Attack : attacker.SpAttack;
            float defenseStat = (moveData.MoveType == CustomEnums.MoveType.Physical) ? target.Defense : target.SpDefense;

            float baseDamage = (((2f * attacker.Level / 5f + 2f) * moveData.power * (attackStat / defenseStat)) / 50f) + 2f;
            damage = baseDamage * Random.Range(0.85f, 1f); // Variation aléatoire
        }

        target.ChangeHP(-Mathf.FloorToInt(damage));
        uiController.Log($"{target.Name} reçoit {Mathf.FloorToInt(damage)} dégâts. PV restants : {target.CurrentHP}");
    }

    private void EndBattle()
    {
        if (state == CombatState.WON) uiController.Log("Victoire !");
        else if (state == CombatState.LOST) uiController.Log("Défaite...");
    }

    private bool isGameFinished()
    {
        return playerPokemon.IsKO() || enemyPokemon.IsKO();
    }
}