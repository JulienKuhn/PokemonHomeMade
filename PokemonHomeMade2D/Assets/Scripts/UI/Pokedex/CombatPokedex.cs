using UnityEngine;
using UnityEngine.UI;

public class CombatPokedex : MonoBehaviour
{
    [SerializeField] private Button Teambtn, Inventorybtn, Scannerbtn, Logbtn;
    [SerializeField] private GameObject TeamPanel, InventoryPanel, ScannerPanel, LogPanel;
    [SerializeField] private Color nonSelectedColor, selectedColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Teambtn.onClick.AddListener(() => this.OpenPanel(1));
        Inventorybtn.onClick.AddListener(() => this.OpenPanel(2));
        Scannerbtn.onClick.AddListener(() => this.OpenPanel(3));
        Logbtn.onClick.AddListener(() => this.OpenPanel(4));

        OpenPanel(1);
    }

    public void OpenPanel(int panelNumber)
    {
        TeamPanel.SetActive(panelNumber==1);
        InventoryPanel.SetActive(panelNumber == 2);
        ScannerPanel.SetActive(panelNumber == 3);
        LogPanel.SetActive(panelNumber == 4);

        Teambtn.image.color = panelNumber == 1? selectedColor : nonSelectedColor;
        Inventorybtn.image.color = panelNumber == 2 ? selectedColor : nonSelectedColor;
        Scannerbtn.image.color = panelNumber == 3 ? selectedColor : nonSelectedColor;
        Logbtn.image.color = panelNumber == 4 ? selectedColor : nonSelectedColor;
    }
}
