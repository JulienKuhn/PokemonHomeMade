using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObjective", menuName = "Quest/Objective/Dialogue")]
public class DialogueObjective : QuestObjective
{
    public int dialogueID;

    public override void StartObjective()
    {
        DialogueManager.instance.OnDialogueEnd = this.OnDialogueEnd;
        DialogueManager.instance.StartNewDialogue(dialogueID);
    }
    private void OnDialogueEnd()
    {
        DialogueManager.instance.OnDialogueEnd = null;
        OnObjectiveComplete?.Invoke();
    }
}
