using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkToObjective", menuName = "Quest/Objective/TalkToObjective")]
public class TalkToObjective : QuestObjective
{
    public int RelatedMapID;
    public int NpcID;
    public int dialogueID;

    public override void StartObjective(QuestData relatedQuest)
    {
        MapManager.instance.OnMapLoaded += this.OnMapLoaded;
    }
    public void OnMapLoaded(int mapId)
    {
        if (mapId == RelatedMapID)
        {
            MapController map = MapManager.instance.GetCurrentMap();
            NPCController npc =  map.GetNPC(NpcID);
            npc.OnNPCInteracted += this.OnInteractionDone;
            Debug.Log("Setup");
        }
    }

    private void OnInteractionDone()
    {
        Debug.Log("OnInteractionDone");
        DialogueManager.instance.OnDialogueEnd = this.OnDialogueEnd;
        DialogueManager.instance.StartNewDialogue(dialogueID);
    }

    private void OnDialogueEnd(int? id)
    {
        DialogueManager.instance.OnDialogueEnd = null;
        OnObjectiveComplete?.Invoke();
    }
}
