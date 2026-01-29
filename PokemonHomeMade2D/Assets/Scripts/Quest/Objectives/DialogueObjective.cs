using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObjective", menuName = "Quest/Objective/Dialogue")]
public class DialogueObjective : QuestObjective
{
    public int dialogueID;

    public override void StartObjective(QuestData relatedQuest)
    {
        DialogueManager.instance.OnDialogueEnd = this.OnDialogueEnd;
        DialogueManager.instance.StartNewDialogue(dialogueID);
    }
    private void OnDialogueEnd(int? answer)
    {
        if(answer == null)
        {
            DialogueManager.instance.OnDialogueEnd = null;
            OnObjectiveComplete?.Invoke();
        }
        else
        {
            DialogueData dialogue = DialogueManager.instance.GetDialogueById(dialogueID);
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
            }
        }

    }
}
