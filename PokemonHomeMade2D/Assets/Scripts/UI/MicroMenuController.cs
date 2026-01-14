using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MicroMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainFrame;

    [SerializeField] private Button pokemonBTN, inventoryBTN, pokedexBTN, notesBTN, saveBTN, settingsBTN, closeBTN;
    [SerializeField] private GameObject pokemonPanel, inventoryPanel, pokedexPanel, notesPanel, settingsPanel;

    public Action OnClose;

    [Header("Settings")]
    [SerializeField] private Button QuitGameBTN;

    private enum Panel
    {
        Pokemon,
        Inventory,
        Pokedex,
        Notes,
        Settings,
        None
    }

    private Panel currentPanel = Panel.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainFrame.SetActive(false);
        pokemonBTN.onClick.AddListener(() => this.OpenPanel(Panel.Pokemon));
        inventoryBTN.onClick.AddListener(() => this.OpenPanel(Panel.Inventory));
        pokedexBTN.onClick.AddListener(() => this.OpenPanel(Panel.Pokedex));
        notesBTN.onClick.AddListener(() => this.OpenPanel(Panel.Notes));
        settingsBTN.onClick.AddListener(() => this.OpenPanel(Panel.Settings));
        saveBTN.onClick.AddListener(() => Debug.Log("Saving"));
        closeBTN.onClick.AddListener(() => OnClose?.Invoke());

        QuitGameBTN.onClick.AddListener(() => Application.Quit());
    }

    private void OpenPanel(Panel panel)
    {
        currentPanel = currentPanel == panel ? Panel.None : panel;

        pokemonPanel.SetActive(currentPanel == Panel.Pokemon);
        inventoryPanel.SetActive(currentPanel == Panel.Inventory);
        pokedexPanel.SetActive(currentPanel == Panel.Pokedex);
        notesPanel.SetActive(currentPanel == Panel.Notes);
        settingsPanel.SetActive(currentPanel == Panel.Settings);
    }

    public void ToggleMenu(bool isOpening)
    {
        OpenPanel(Panel.None);
        mainFrame.SetActive(isOpening);
    }
}
