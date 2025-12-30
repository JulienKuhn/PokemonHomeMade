using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CheatController : MonoBehaviour
{
    [SerializeField] private CanvasGroup cheatGroup;
    [SerializeField] private TextMeshProUGUI historyField;
    [SerializeField] private TMP_InputField cmdField;

    private List<string> previousCMDs = new List<string>();
    private int previousIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
        cheatGroup.gameObject.SetActive(false);
    }

    void Update()
    {
        // On récupère le clavier actuel
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // 1. Touche '²' (Backquote) pour ouvrir/fermer
        if (keyboard.backquoteKey.wasPressedThisFrame)
        {
            ToggleConsole();
        }

        // 2. Touche 'Entrée' pour envoyer (si la console est ouverte)
        if (cheatGroup.gameObject.activeSelf && (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame))
        {
            SubmitCommand();
        }

        if (cheatGroup.gameObject.activeSelf && (keyboard.upArrowKey.wasPressedThisFrame || keyboard.downArrowKey.wasPressedThisFrame))
        {
            SetPreviousCMD(keyboard.upArrowKey.wasPressedThisFrame);
        }
    }
    private void ToggleConsole()
    {
        bool isOpening = !cheatGroup.gameObject.activeSelf;
        cheatGroup.gameObject.SetActive(isOpening);

        if (isOpening)
        {
            cmdField.text = ""; // Clear previous command
            cmdField.ActivateInputField(); // Focus the field so you can type immediately
            cmdField.Select();
        }
        else
        {
            // Optional: Resume game time if you paused it
            // Time.timeScale = 1; 
        }
    }

    private void SubmitCommand()
    {
        string input = cmdField.text;
        if (!string.IsNullOrWhiteSpace(input))
        {
            Debug.Log(input + " !!!!!!!");
            DoCommand(input);
        }

        cmdField.text = ""; // Clear previous command
        cmdField.ActivateInputField(); // Focus the field so you can type immediately
        cmdField.Select();
        //cheatGroup.gameObject.SetActive(false);
    }

    private void SetPreviousCMD(bool isPrevious)
    {
        previousIndex += isPrevious ? -1 : 1;
        Debug.Log(previousIndex);
        previousIndex = Math.Clamp(previousIndex, 0, previousCMDs.Count - 1);
        cmdField.text = previousCMDs[previousIndex];
    }

    private void DoCommand(string cmd)
    {
        cmd = cmd.ToLower();
        historyField.text += $"\n <i> {cmd} </i> \n";
        previousCMDs.Add(cmd);
        previousIndex = previousCMDs.Count;
        switch (cmd)
        {
            case "chocopouce":
                if(CombatController.Instance is null)
                {
                    historyField.text += "<b> There is nothing to kill </b>";
                }
                else
                {
                    CombatController.Instance.KillOpponent();
                    historyField.text += "<b> Killing Opponent </b>";
                }
                break;
            default:
                historyField.text += "<color=\"red\"> Command is unkown </color>";
                break;
        }
    }
}
