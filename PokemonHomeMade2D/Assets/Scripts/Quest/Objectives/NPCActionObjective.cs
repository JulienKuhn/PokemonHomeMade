using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCActionObjective", menuName = "Quest/Objective/NPCAction")]
public class NPCActionObjective : QuestObjective
{
    public enum ActionType
    {
        Move,
        Show,
        Hide,
        Exclamation,
    }

    [EnumToggleButtons]
    public ActionType Action;

    [Header("NPC Definition")]
    public int RelatedMapID;
    public int NpcID;

    [ShowIf("Action", ActionType.Move)]
    public NPCMoveData MoveData;

    private MapController map;
    private NPCController npc;

    public override void StartObjective(QuestData relatedQuest)
    {

        switch (Action)
        {
            case ActionType.Move:
                SetupNPC();
                npc.Move(MoveData.Points, MoveData.TravelTimePerUnit, MoveData.MovementEase);
                break;
            case ActionType.Show:
                NPCManager.instance.ChangeNPCHiddenStatus(NpcID, true);
                OnObjectiveComplete?.Invoke();
                break;
            case ActionType.Hide:
                NPCManager.instance.ChangeNPCHiddenStatus(NpcID, false);
                OnObjectiveComplete?.Invoke();
                break;
            case ActionType.Exclamation:
                SetupNPC();
                npc.DoExclamationMark();
                break;
        }

    }

    private void SetupNPC()
    {
        map = MapManager.instance.GetCurrentMap();
        npc = map.GetNPC(NpcID);
        npc.OnMovePerformed = this.OnMovePerformed;
    }

    private void OnMovePerformed()
    {
        if (MoveData.DisappearOnEnd)
            NPCManager.instance.ChangeNPCHiddenStatus(NpcID, false);

        OnObjectiveComplete?.Invoke();
    }
}

[System.Serializable]
public class NPCMoveData
{
    public List<Vector3> Points;
    public float TravelTimePerUnit;
    public Ease MovementEase;
    public bool DisappearOnEnd;
}
