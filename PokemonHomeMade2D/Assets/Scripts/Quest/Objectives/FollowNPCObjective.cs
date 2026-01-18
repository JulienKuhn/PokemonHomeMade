using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "FollowNPCObjective", menuName = "Quest/Objective/FollowNPCObjective")]
public class FollowNPCObjective : QuestObjective
{
    [SerializeField] private float TravelTimePerUnit;
    [SerializeField] private int NpcID;

    [SerializeField] private List<Vector3> NPCPathway;
    [SerializeField] private List<Vector3> PlayerPathway;

    public override void StartObjective()
    {
        MapController map = MapManager.instance.GetCurrentMap();
        NPCController npc = map.GetNPC(NpcID);

        npc.Move(NPCPathway, TravelTimePerUnit, Ease.Linear);
        GameManager.instance.MovePlayerToLocations(PlayerPathway, ()=> OnObjectiveComplete?.Invoke());
    }
}
