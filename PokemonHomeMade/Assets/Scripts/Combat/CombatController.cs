using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour
{
    public static CombatController Instance;
    public bool isGameOver;
    public bool isTurnOnGoing;
    private bool isVictory;

    // test values
    public PokemonData testpok;
    private PokemonEntity CurrentOpponentPokemon;
    private PokemonEntity CurrentPlayerPokemon;

    private PokemonSpell nextSpell = null;
    [SerializeField] private SelectionMenuController selectionMenuController;
    [SerializeField] private CombatPanelController combatPanelController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        CurrentOpponentPokemon = new PokemonEntity(testpok, 5);
        CurrentPlayerPokemon = new PokemonEntity(testpok, 4);

        // Start Combat Opening Animation
        StartCoroutine(OpenCombat());
    }

    private IEnumerator OpenCombat()
    {
        Debug.Log("Opent Combat");

        selectionMenuController.DoDescription($"Adversaire fait appel à {CurrentOpponentPokemon.Name}");
        combatPanelController.StartOpponentOpening(CurrentOpponentPokemon.Name, CurrentOpponentPokemon.Level, CurrentOpponentPokemon.CurrentHP / CurrentOpponentPokemon.MaxHP);
        yield return new WaitForSeconds(5f);

        selectionMenuController.DoDescription($"{CurrentPlayerPokemon.Name} en avant !");
        combatPanelController.StartPlayerOpening(CurrentPlayerPokemon.Name, CurrentPlayerPokemon.Level, CurrentPlayerPokemon.CurrentHP / CurrentPlayerPokemon.MaxHP, CurrentPlayerPokemon.CurrentExp);
        yield return new WaitForSeconds(3f);

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
            selectionMenuController.DoDescription($"Victoire ! Nouvelle partie dans 5 secondes");
        }
        else
        {
            // Do GameOver Screen
            selectionMenuController.DoDescription($"Defaite ! Nouvelle partie dans 5 secondes");
            // Teleport Player to Latest medical center
            // Start respawn dialogue
        }

        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private IEnumerator DoPlayerTurn()
    {
        if (!isGameOver)
        {
            Debug.Log("DoPlayerTurn");
            isTurnOnGoing = true;
            yield return null;
            selectionMenuController.OpenChoices(CurrentPlayerPokemon.LearnedSpells, $"Que doit faire {CurrentPlayerPokemon.Name} ?");
            nextSpell = null;
            selectionMenuController.OnSpellSelected += OnActionRecieved;
            yield return new WaitWhile(() => nextSpell == null);

            if (CurrentPlayerPokemon.LearnedSpells.ContainsKey(nextSpell))
            {
                CurrentPlayerPokemon.LearnedSpells[nextSpell]--;

                // Optionnel : s'assurer que les PP ne tombent pas sous 0
                if (CurrentPlayerPokemon.LearnedSpells[nextSpell] < 0)
                    CurrentPlayerPokemon.LearnedSpells[nextSpell] = 0;
            }

            selectionMenuController.DoDescription($"{CurrentPlayerPokemon.Name} lance {nextSpell.name} !");
            yield return new WaitForSeconds(1f);
            if (nextSpell.category != SpellCategory.Status)
            {
                CurrentOpponentPokemon.ChangeHP(nextSpell.power * -1);
                float newAmount = (float)CurrentOpponentPokemon.CurrentHP / (float)CurrentOpponentPokemon.MaxHP;
                combatPanelController.ChangeOpponentLifeAmount(newAmount);
            }

            yield return null;

            isTurnOnGoing = false;
        }
    }

    private void OnActionRecieved(PokemonSpell spell)
    {
        selectionMenuController.OnSpellSelected = null;
        nextSpell = spell;
    }

    private IEnumerator DoComputerTurn()
    {
        if (!isGameOver)
        {
            Debug.Log("DoComputerTurn");
            isTurnOnGoing = true;
            yield return null;

            selectionMenuController.DoDescription($"Au tour du {CurrentOpponentPokemon.Name} adverse");
            yield return new WaitForSeconds(2f);

            int rand = UnityEngine.Random.Range(0, 100);
            if(rand > 60)
            {
                selectionMenuController.DoDescription($"{CurrentOpponentPokemon.Name} adverse ne fait rien");
            }
            else
            {
                selectionMenuController.DoDescription($"{CurrentOpponentPokemon.Name} adverse utilise Griffure");

                CurrentPlayerPokemon.ChangeHP(-5);
                float newAmount = (float)CurrentPlayerPokemon.CurrentHP / (float)CurrentPlayerPokemon.MaxHP;
                combatPanelController.ChangePlayerLifeAmount(newAmount);
            }
            yield return new WaitForSeconds(2f);

            yield return null;
            isTurnOnGoing = false;
        }
    }

    public bool isPlayerStartingTurn()
    {
        if (CurrentPlayerPokemon.Speed == CurrentOpponentPokemon.Speed)
        {
            int rand = UnityEngine.Random.Range(0, 100);
            return rand > 50;
        }

        return CurrentPlayerPokemon.Speed >= CurrentOpponentPokemon.Speed;
    }

    public bool CheckIfGameIsOver()
    {
        if (CurrentPlayerPokemon.CurrentHP <= 0)
        {
            selectionMenuController.DoDescription($"{CurrentPlayerPokemon.Name} est vaincu");
            combatPanelController.RecallPlayerPokemon();
            isGameOver = true;
            isVictory = false;
        }

        if (CurrentOpponentPokemon.CurrentHP <= 0)
        {
            selectionMenuController.DoDescription($"{CurrentOpponentPokemon.Name} est vaincu");
            combatPanelController.RecallOpponentPokemon();
            isGameOver = true;
            isVictory = true;
        }
        return isGameOver;
    }
    public void KillOpponent()
    {
        CurrentOpponentPokemon.ChangeHP(-9999999);
        combatPanelController.ChangeOpponentLifeAmount(0f);
        isTurnOnGoing = false;
    }
}
