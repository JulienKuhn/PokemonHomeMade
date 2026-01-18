using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceDialogueObjective", menuName = "Quest/Objective/ChoiceDialogueObjective")]
public class ChoiceDialogueObjective : QuestObjective
{
    [SerializeField] private List<ChoiceDialogueOption> options;
    private ChoiceDialogueOption selectedOption;

    public override void StartObjective()
    {
        DialogueManager.instance.OnDialogueEnd = this.OnDialogueEnd;

        MapController map = MapManager.instance.GetCurrentMap();
            
        foreach(ChoiceDialogueOption option in options)
        {
            var npc = map.GetNPC(option.NPCID);
            npc.OnNPCInteracted += ()=> this.OnNPCInteracted(option.NPCID);
            npc.SetInteractable(true);
        }
    }

    private void OnNPCInteracted(int npcID)
    {
        selectedOption = options.Where((o)=> o.NPCID == npcID).FirstOrDefault();
        DialogueManager.instance.StartNewDialogue(selectedOption.DialogueID);
    }

    private void OnDialogueEnd(int? answer)
    {
        if (answer == null)
        {
            DialogueManager.instance.OnDialogueEnd = null;
            OnObjectiveComplete?.Invoke();
        }
        else
        {
            DialogueData dialogue = DialogueManager.instance.GetDialogueById(selectedOption.DialogueID);
            Debug.Log(answer.Value);
            DialogueQuestion question = dialogue.Questions[answer.Value];
            switch (question.Action)
            {
                case DialogueQuestion.DialogueAction.None:
                    DialogueManager.instance.QuitDialogue();
                    OnObjectiveComplete?.Invoke();
                    break;
                case DialogueQuestion.DialogueAction.GiveItem:
                    DialogueManager.instance.QuitDialogue();
                    OnObjectiveComplete?.Invoke();
                    break;
                case DialogueQuestion.DialogueAction.GiveCurrency:
                    DialogueManager.instance.QuitDialogue();
                    OnObjectiveComplete?.Invoke();
                    break;
                case DialogueQuestion.DialogueAction.Quit:
                    DialogueManager.instance.QuitDialogue();
                    break;
                case DialogueQuestion.DialogueAction.NewDialogue:
                    DialogueManager.instance.OnDialogueEnd = this.OnDialogueEnd;
                    DialogueManager.instance.StartNewDialogue(int.Parse(question.Parameters[0]));
                    break;
                case DialogueQuestion.DialogueAction.GivePokemon:
                    Debug.Log("Give Pokemon with ID " +  question.Parameters[0]);
                    DialogueManager.instance.QuitDialogue();
                    OnObjectiveComplete?.Invoke();
                    break;
            }
        }

    }
}

[Serializable]
public class ChoiceDialogueOption
{
    public int NPCID;
    public int DialogueID;
}
