using UnityEngine;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour
{
    public TextAsset mapFile;

    [SerializeField] private RectTransform gridUI; // Le parent qui a le GridLayoutGroup
    [SerializeField] private Sprite basic0, basic1, basic2, basic3;

    private string[,] grid;

    void Start()
    {
        if (mapFile != null)
        {
            LoadGrid(mapFile.text);
        }
    }

    public void LoadGrid(string rawText)
    {
        string[] lines = rawText.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        int rowCount = lines.Length;
        int colCount = lines[0].Split(',').Length;

        grid = new string[rowCount, colCount];

        // --- CORRECTION DES DIMENSIONS ---
        // Largeur = Colonnes * TailleCellule | Hauteur = Lignes * TailleCellule
        gridUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, colCount * 32);
        gridUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rowCount * 32);

        // --- RÉGLAGE DE LA GRILLE ---
        GridLayoutGroup layout = gridUI.GetComponent<GridLayoutGroup>();
        if (layout != null)
        {
            layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layout.constraintCount = colCount; // Force le retour à la ligne au bon moment
            layout.cellSize = new Vector2(32, 32);
        }

        for (int y = 0; y < rowCount; y++)
        {
            string[] values = lines[y].Split(',');
            for (int x = 0; x < colCount; x++)
            {
                string val = values[x].Trim();
                grid[y, x] = val;
                AddImageToGrid(val);
            }
        }

        Debug.Log($"Grille chargée : {rowCount} lignes x {colCount} colonnes.");
    }

    public void AddImageToGrid(string value)
    {
        GameObject newObject = new GameObject("Tile", typeof(RectTransform), typeof(Image));
        newObject.transform.SetParent(gridUI, false);

        Image img = newObject.GetComponent<Image>();

        // Utilisation d'un switch pour assigner le sprite
        switch (value)
        {
            case "0": img.sprite = basic0; break;
            case "1": img.sprite = basic1; break;
            case "2": img.sprite = basic2; break;
            case "3": img.sprite = basic3; break;
            default: img.color = Color.clear; break; // Cache l'image si la valeur est inconnue
        }
    }
}