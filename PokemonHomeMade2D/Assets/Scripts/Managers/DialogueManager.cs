using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private DialogueUIController controller;
    [SerializeField] private List<DialogueData> dialogues;

    public Action<int?> OnDialogueEnd;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    public void StartNewDialogue(int id )
    {
        controller.StartDialogue(dialogues[id]);
    }

    public void QuitDialogue()
    {
        controller.Quit();
    }

    public DialogueData GetDialogueById(int id)
    {
        return dialogues[id];
    }
}
