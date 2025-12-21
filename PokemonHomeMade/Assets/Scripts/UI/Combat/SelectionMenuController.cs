using DG.Tweening;
using System;
using System.Collections;
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

    private PokemonSpell[] spells = new PokemonSpell[4];

    public Action<PokemonSpell> OnSpellSelected;

    private Coroutine descriptionCoroutine = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        descriptionField.text = "";
        choicePanel.SetActive(true);
        spellPanel.SetActive(false);

        fightBTN.onClick.AddListener(OpenFight);
        bagBTN.onClick.AddListener(OpenBag);
        runBTN.onClick.AddListener(TryToRun);

        spell01.onClick.AddListener(() => OnSpellSelected.Invoke(spells[0]));
        spell02.onClick.AddListener(() => OnSpellSelected.Invoke(spells[1]));
        spell03.onClick.AddListener(() => OnSpellSelected.Invoke(spells[2]));
        spell04.onClick.AddListener(() => OnSpellSelected.Invoke(spells[3]));
    }

    public void OpenChoices(PokemonSpell[] newSpells, string description)
    {
        descriptionField.text = "";
        descriptionField.DOText(description, .4f);
        spells = newSpells;
        SetupSpells();
        choicePanel.SetActive(true);
        spellPanel.SetActive(false);
    }

    private void SetupSpells()
    {
        bool isSpell = spells[0] != null;
        spell01.gameObject.SetActive(isSpell);
        if (isSpell) spell01txt.text = spells[0].moveName;

        isSpell = spells[1] != null;
        spell02.gameObject.SetActive(isSpell);
        if (isSpell) spell02txt.text = spells[1].moveName;

        isSpell = spells[2] != null;
        spell03.gameObject.SetActive(isSpell);
        if (isSpell) spell03txt.text = spells[2].moveName;

        isSpell = spells[3] != null;
        spell04.gameObject.SetActive(isSpell);
        if (isSpell) spell04txt.text = spells[3].moveName;
    }

    private void OpenFight()
    {
        choicePanel.SetActive(false);
        spellPanel.SetActive(true);
    }

    private void OpenBag()
    {
        DoDescription("Fonction OpenBag non implémentée");
        Debug.Log("OpenBAG");
    }

    private void TryToRun()
    {
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
}
