using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;

    [SerializeField] private GameObject worldRoot;
    [SerializeField] private int xMapSize;

    private Dictionary<Vector2, TileController> tilesDict;

    void Awake()
    {
        if (instance == null) instance = this;
        tilesDict = new Dictionary<Vector2, TileController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        int x = 0;
        int y = 0;

        TileController[] foundTiles = worldRoot.GetComponentsInChildren<TileController>();

        foreach (TileController tc in foundTiles)
        {
            Vector2Int pos = new Vector2Int(x, y);

            tc.gameObject.name = $"tile_{x}:{y}";

            if (!tilesDict.ContainsKey(pos))
                tilesDict.Add(pos, tc);

            x++;
            if (x >= xMapSize)
            {
                x = 0;
                y++;
            }
        }
    }

    public TileController GetTileByCoords(Vector2 coords)
    {
        if (tilesDict.TryGetValue(coords, out TileController tile))
        {
            return tile;
        }
        return null;
    }
}
