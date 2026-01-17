using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnLocations;
    [SerializeField] private List<NPCController> Npcs;

    [Header("Camera Settings")]
    [SerializeField] private bool isCameraLocked;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    public bool hasStarted=false;

    public void StartMap(int spawnLopcationID = 0)
    {
        GameManager.instance.TeleportPlayerToLocation(spawnLocations[spawnLopcationID]);

        if (isCameraLocked)
            CameraManager.instance.LockCameraOnPlayer();
        else
            CameraManager.instance.SetupCamera(minBounds, maxBounds);

        hasStarted = true;
    }

    public NPCController GetNPC(int id)
    {
        NPCController npc = Npcs.Where(n => n.NPCID == id).FirstOrDefault();
        return npc;
    }

}
