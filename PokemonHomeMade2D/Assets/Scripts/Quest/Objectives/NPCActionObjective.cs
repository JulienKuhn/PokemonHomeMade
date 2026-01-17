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
    }

    [EnumToggleButtons]
    public ActionType Action;

    [Header("NPC Definition")]
    public int RelatedMapID;
    public int NpcID;

    [ShowIf("Action", ActionType.Move)]
    public NPCMoveData MoveData;

    public override void StartObjective()
    {
        switch (Action)
        {
            case ActionType.Move:
                MapController map = MapManager.instance.GetCurrentMap();
                NPCController npc = map.GetNPC(NpcID);
                npc.OnMovePerformed = this.OnMovePerformed;
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
        }

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
