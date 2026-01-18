using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerController player;
    public GameSave currentSave = new GameSave();
    public bool CanChangeMap = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeMap(0, 0);
    }

    public void ChangeMap(int mapID, int spawnLocationID)
    {
        if (!CanChangeMap) return;

        player.StopMovement();
        MapManager.instance.ChangeMap(mapID, spawnLocationID);
    }

    public void FreezePlayer()
    {
        player.Freeze();
    }

    public void UnFreezePlayer()
    {
        player.UnFreeze();
    }

    public void MovePlayerToLocations(List<Vector3> pos, Action callback)
    {
        player.MovePlayerToLocations(pos, callback);
    }

    public void TeleportPlayerToLocation(Vector3 location)
    {
        player.gameObject.transform.position = location;
    }

    public void TriggerCustomEvent(TriggerableEvent.EventType eventType, string[] eventConditions = null)
    {
        switch (eventType)
        {
            case TriggerableEvent.EventType.ChangeMap:
                Debug.Log("new map " + eventConditions[0] + "-" + eventConditions[1]);
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
    public void Save()
    {
        string jsonData = JsonUtility.ToJson(currentSave);
        PlayerPrefs.SetString($"Save{currentSave.SaveID}", jsonData);
    }

    public GameSave? GetGameSaveByID(int id)
    {
        if (!PlayerPrefs.HasKey($"Save{id}"))
        {
            Debug.Log($"Save{id} doesnt exist");
            return null;
        }
        string jsonData = PlayerPrefs.GetString($"Save{id}");
        return JsonUtility.FromJson<GameSave>(jsonData);
    }

    public void LoadSaveByID(int id)
    {
        if (!PlayerPrefs.HasKey($"Save{id}"))
        {
            Debug.LogError($"Save{id} doesnt exist");
            return;
        }
        string jsonData = PlayerPrefs.GetString($"Save{id}");
        currentSave = JsonUtility.FromJson<GameSave>(jsonData);

        SceneManager.LoadScene("SampleScene");
    }
}
