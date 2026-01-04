using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NewGameController : MonoBehaviour
{

    [Header("CharacterSelectionPanel")]
    [SerializeField] private GameObject CharacterSelectionPanel;
    [SerializeField] private Button MaleBTN, FemaleBTN;

    [Header("NamingPanel")]
    [SerializeField] private GameObject NamingPanel;
    [SerializeField] private TMP_InputField nameInputfield;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaleBTN.onClick.AddListener(() => OnVisualSelected(0));
        FemaleBTN.onClick.AddListener(() => OnVisualSelected(0));
        CharacterSelectionPanel.SetActive(true);
        NamingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (NamingPanel.activeSelf && keyboard.enterKey.wasPressedThisFrame && nameInputfield.text.Length > 0) 
        {
            ValidateName();
        }
    }

    private void OnVisualSelected(int visual)
    {
        GameManager.Instance.currentSave.CharacterVisual = visual;
        CharacterSelectionPanel.SetActive(false);
        NamingPanel.SetActive(true);
    }

    private void ValidateName()
    {
        GameManager.Instance.currentSave.PlayerName = nameInputfield.text;
        NamingPanel.SetActive(false);
        GameManager.Instance.Save();
        GameManager.Instance.Reload();
    }
}
