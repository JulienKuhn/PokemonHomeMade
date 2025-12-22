using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionField;

    [Header("Spell Panel")]
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button fightBTN, bagBTN, runBTN;

    [Header("Spell Panel")]
    [SerializeField] private GameObject spellPanel;
    [SerializeField] private Button spell01, spell02, spell03, spell04;
    [SerializeField] private TextMeshProUGUI spell01txt, spell02txt, spell03txt, spell04txt;
    [SerializeField] private TextMeshProUGUI pp01txt, pp02txt, pp03txt, pp04txt;

    private Dictionary<PokemonSpell, int> LearnedSpells;

    public Action<PokemonSpell> OnSpellSelected;

    private Coroutine descriptionCoroutine = null;
    private bool canSwapPanel = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        descriptionField.text = "";
        choicePanel.SetActive(true);
        spellPanel.SetActive(false);

        fightBTN.onClick.AddListener(OpenFight);
        bagBTN.onClick.AddListener(OpenBag);
        runBTN.onClick.AddListener(TryToRun);

        spell01.onClick.AddListener(() => OnSpellChoosen(0));
        spell02.onClick.AddListener(() => OnSpellChoosen(1));
        spell03.onClick.AddListener(() => OnSpellChoosen(2));
        spell04.onClick.AddListener(() => OnSpellChoosen(3));
    }

    public void OpenChoices(Dictionary<PokemonSpell, int> learnedSpells, string description)
    {
        descriptionField.text = "";
        descriptionField.DOText(description, .4f);
        LearnedSpells = learnedSpells;
        SetupSpells();
        choicePanel.SetActive(true);
        spellPanel.SetActive(false);
        canSwapPanel = true;
    }

    private void SetupSpells()
    {
        spell01.gameObject.SetActive(false);
        spell02.gameObject.SetActive(false);
        spell03.gameObject.SetActive(false);
        spell04.gameObject.SetActive(false);

        int i = 0;
        foreach (var spell in LearnedSpells)
        {
            if (i == 0)
            {
                spell01.gameObject.SetActive(true);
                spell01.interactable = spell.Value > 0;
                spell01txt.text = spell.Key.moveName;
                pp01txt.text = spell.Value.ToString() + "/" + spell.Key.maxPP;
            }
            else if (i == 1)
            {
                spell02.gameObject.SetActive(true);
                spell02.interactable = spell.Value > 0;
                spell02txt.text = spell.Key.moveName;
                pp02txt.text = spell.Value.ToString() + "/" + spell.Key.maxPP;
            }
            else if (i == 2)
            {
                spell03.gameObject.SetActive(true);
                spell03.interactable = spell.Value > 0;
                spell03txt.text = spell.Key.moveName;
                pp03txt.text = spell.Value.ToString() + "/" + spell.Key.maxPP;
            }
            else if (i == 3)
            {
                spell04.gameObject.SetActive(true);
                spell04.interactable = spell.Value > 0;
                spell04txt.text = spell.Key.moveName;
                pp04txt.text = spell.Value.ToString() + "/" + spell.Key.maxPP;
            }
            i++;
        }
    }

    private void OpenFight()
    {
        if (!canSwapPanel) return;

        choicePanel.SetActive(false);
        spellPanel.SetActive(true);
    }

    private void OpenBag()
    {
        if (!canSwapPanel) return;

        DoDescription("Fonction OpenBag non implémentée");
        Debug.Log("OpenBAG");
    }

    private void TryToRun()
    {
        if (!canSwapPanel) return;

        DoDescription("Fonction TryToRun non implémentée");
        Debug.Log("TryToRun");
    }

    public void DoDescription(string description) 
    {
        if(descriptionCoroutine != null)
            StopCoroutine(descriptionCoroutine);

        descriptionCoroutine = StartCoroutine(DoDescriptionCo(description));
    }

    private IEnumerator DoDescriptionCo(string description)
    {
        descriptionField.text = "";
        yield return null;
        descriptionField.DOText(description, .4f);
        yield return new WaitForSeconds(.5f);
        descriptionCoroutine = null;
    }

    private void OnSpellChoosen(int spellID)
    {
        // Toujours vérifier si l'index est valide pour éviter un crash
        if (spellID < 0 || spellID >= LearnedSpells.Count) return;

        canSwapPanel = false;
        choicePanel.SetActive(true);
        spellPanel.SetActive(false);

        // .ElementAt(index) permet de simuler un accès par index sur les clés
        PokemonSpell selectedSpell = LearnedSpells.Keys.ElementAt(spellID);

        // Utilise ?.Invoke pour éviter une erreur si personne n'écoute l'Action
        OnSpellSelected?.Invoke(selectedSpell);
    }
}
