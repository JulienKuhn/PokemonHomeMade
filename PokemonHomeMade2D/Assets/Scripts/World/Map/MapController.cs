using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnLocations;

    public void StartMap(int spawnLopcationID = 0)
    {
        GameManager.instance.TeleportPlayerToLocation(spawnLocations[spawnLopcationID]);
    }
}
