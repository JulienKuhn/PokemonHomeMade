using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using System.Collections;
using NUnit.Framework;
using Sirenix.Utilities;

public class CombatUIController : MonoBehaviour
{
    [Header("ActionPanel")]
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private CanvasGroup globalGroup, menuGroup, moveGroup;
    [SerializeField] private MoveButtonUI[] moveButtons;
    [SerializeField] private TextMeshProUGUI logText;

    public Action<int> OnMoveSelected;
    private bool CanInteract = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StopCombat();
        moveButtons[0].OnClick += () => this.OnSelection(0);
        moveButtons[1].OnClick += () => this.OnSelection(1);
        moveButtons[2].OnClick += () => this.OnSelection(2);
        moveButtons[3].OnClick += () => this.OnSelection(3);
    }

    public void StartCombat(PokemonEntity playerPokemon, Action callback)
    {

        for (int i = 0; i < moveButtons.Length; i++) {
            if (playerPokemon.LearnedMoves.Count < i + 1)
            {
                moveButtons[i].Disable();
            }
            else
            {
                var move = playerPokemon.LearnedMoves[i];
                moveButtons[i].Setup(move.Item1.moveName, $"{move.Item2}/{move.Item1.MainMove.maxPP}", move.Item1.moveType);
            }
        }
        StartCoroutine(DoStartCombat(callback));
    }

    private IEnumerator DoStartCombat(Action callback)
    {
        globalGroup.alpha = 1.0f;
        globalGroup.interactable = true;
        globalGroup.blocksRaycasts = true;
        yield return new WaitForSeconds(1);
        callback?.Invoke();
    }

    public void StartTurn()
    {
        CanInteract = true;
    }

    public void OnSelection(int moveId)
    {
        if (!CanInteract) return;
        this.OnMoveSelected?.Invoke(moveId);
        CanInteract = false;
    }

    public void StopCombat()
    {
        globalGroup.alpha = 0.0f;
        globalGroup.interactable =false;
        globalGroup.blocksRaycasts = false;
    }

    public void Log(string message)
    {
        logText.text += $"\n {message}";
    }
}
