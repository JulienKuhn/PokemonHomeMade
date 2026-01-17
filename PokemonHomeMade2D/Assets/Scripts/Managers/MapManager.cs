using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    [SerializeField] private List<MapController> maps;
    [SerializeField] private MapController currentMap;

    public Action<int> OnMapLoaded;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NPCManager.instance.OnNPCStatusChanged += this.OnNPCStatusChanged;
    }

    private void OnNPCStatusChanged(int npcID, bool show)
    {
        if (currentMap != null) 
        {
            NPCController npc = currentMap.GetNPC(npcID);
            if (npc == default || npc == null) return;

            if (show)
                npc.ShowNPC();
            else
                npc.HideNPC();
        }
    }

    public void ChangeMap(int mapID, int spawnLocationID)
    {
        if (currentMap != null)
            Destroy(currentMap.gameObject);

        currentMap = Instantiate(maps[mapID]);
        currentMap.StartMap(spawnLocationID);
        OnMapLoaded?.Invoke(mapID);
    }

    public MapController GetCurrentMap()
    {
        return currentMap;
    }
}
