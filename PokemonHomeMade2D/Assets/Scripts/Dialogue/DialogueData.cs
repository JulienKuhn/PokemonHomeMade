using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueData")]
public class DialogueData : ScriptableObject
{
    public bool isTalkingHead;
    public List<DialogueLine> Lines;
    public List<DialogueQuestion> Questions;
}

[Serializable]
public class DialogueLine
{
    public string SpeakerName;
    public string Line;
}

[Serializable]
public class DialogueQuestion
{
    public enum DialogueAction
    {
        None,
        GiveItem,
        GiveCurrency,
        GivePokemon,
        Quit,
        NewDialogue,
    }

    public string QuestionText;
    public DialogueAction Action;
    public string[] Parameters;
}