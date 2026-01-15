using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueUIController controller;
    [SerializeField] private List<DialogueData> dialogues;

    private void Start()
    {
        StartNewDialogue(0);
    }

    public void StartNewDialogue(int id)
    {
        controller.StartDialogue(dialogues[id]);
    }
}
