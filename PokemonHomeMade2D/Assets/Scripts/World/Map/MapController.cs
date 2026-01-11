using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnLocations;

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
}
