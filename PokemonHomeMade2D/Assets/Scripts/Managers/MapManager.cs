using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    [SerializeField] private List<MapController> maps;
    [SerializeField] private MapController currentMap;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeMap(int mapID, int spawnLocationID)
    {
        if (currentMap != null)
            DestroyObject(currentMap.gameObject);

        currentMap = Instantiate(maps[mapID]);
        currentMap.StartMap(spawnLocationID);
    }
}
