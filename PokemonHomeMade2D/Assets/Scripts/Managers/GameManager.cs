using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerController player;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeMap(int mapID, int spawnLocationID)
    {
        player.StopMovement();
        MapManager.instance.ChangeMap(mapID, spawnLocationID);
    }

    public void TeleportPlayerToLocation(Vector3 location)
    {
        player.gameObject.transform.position = location;
    }

    public void TriggerCustomEvent(TriggerableEvent.EventType eventType, string[] eventConditions = null)
    {
        Debug.Log("new event " + eventType.ToString());
        switch (eventType)
        {
            case TriggerableEvent.EventType.ChangeMap:
                ChangeMap(int.Parse(eventConditions[0]), int.Parse(eventConditions[1]));
                break;
            case TriggerableEvent.EventType.Dialogue:
                Debug.Log("Dialogue not implemented");
                break;
            default:
                Debug.LogError($"Event {eventType.ToString()} doesnt exist");
                break;
        }
    }
}
