using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Rendering.MaterialUpgrader;

public class DialogueUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup dialogueGroup;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private Transform dialogueCursor;

    [SerializeField] private CanvasGroup answerGroup;
    [SerializeField] private DialogueButton answerBTNPrefab;

    private List<DialogueButton> instantiatedButtons = new List<DialogueButton>();
    private DialogueData currentDialogue = null;
    private Tweener cursorTweener, textTweener, canvasTweener;
    private bool hasPlayerClicked = false;
    private string currentWritingLine="";
    private bool isLineSkipped = false;
    private int selectedAnswer = 0;

    private Coroutine uiCo = null;
    private void Start()
    {
        dialogueGroup.alpha = 0;
        answerGroup.alpha = 0;
        dialogueCursor.gameObject.SetActive(false);
        dialogueText.text = "";
    }

    public void StartDialogue(DialogueData data)
    {
        if(uiCo != null) 
            StopCoroutine(uiCo);
        if (cursorTweener != null && cursorTweener.IsPlaying())
            cursorTweener.Kill();
        if (textTweener != null && textTweener.IsPlaying())
            textTweener.Kill();
        if (canvasTweener != null && canvasTweener.IsPlaying())
            canvasTweener.Kill();
        currentDialogue = data;
        uiCo = StartCoroutine(DoDialogue());
    }

    private void Update()
    {
        var mouse = Mouse.current;

        if (mouse != null && (mouse.leftButton.wasPressedThisFrame || mouse.rightButton.wasPressedThisFrame))
        {
            if(textTweener != null)
            {
                textTweener.Kill();
                dialogueText.text = currentWritingLine;
                textTweener = null;
                isLineSkipped = true;
            }
            else
            {
                hasPlayerClicked = true;
            }
        }
    }

    private IEnumerator DoDialogue()
    {
        dialogueGroup.DOFade(1, .5f);
        yield return new WaitForSeconds(.8f);
        foreach (var line in currentDialogue.Lines)
        {
            isLineSkipped = false;
            speakerText.text = line.SpeakerName;
            dialogueText.text = "";
            currentWritingLine = line.Line;
            dialogueCursor.gameObject.SetActive(false);
            yield return null;
            float duration = currentWritingLine.Length * 0.03f;
            textTweener = dialogueText.DOText(currentWritingLine, duration);

            float timer = 0;
            while ((timer < duration + .1f) && !isLineSkipped)
            {
                yield return null;
                timer += Time.deltaTime;
            }
            textTweener = null;
            dialogueCursor.gameObject.SetActive(true);
            Coroutine couroutine = StartCoroutine(DoInfiniteBounce());
            hasPlayerClicked = false;

            while (!hasPlayerClicked)
            {
                yield return null;
            }

            if (cursorTweener != null)
                cursorTweener.Kill();

            StopCoroutine(couroutine);
        }
        if (currentDialogue.Questions.Count > 0) 
        {
            selectedAnswer = 999;
            int i = 0;
            foreach (var question in currentDialogue.Questions) 
            {
                DialogueButton btn = Instantiate(answerBTNPrefab, answerBTNPrefab.transform.parent);
                btn.gameObject.SetActive(true);
                btn.Setup(i, question.QuestionText);
                btn.OnButtonClicked += (j) => selectedAnswer = j;
                instantiatedButtons.Add(btn);
                i++;
            }
            answerGroup.DOFade(1, .5f);
            yield return new WaitWhile(() => selectedAnswer == 999);
            DialogueManager.instance.OnDialogueEnd?.Invoke(selectedAnswer);
            canvasTweener = null;
            answerGroup.alpha = 0;
        }
        else
        {
            canvasTweener = dialogueGroup.DOFade(0, .5f);
            yield return new WaitForSeconds(.8f);
            DialogueManager.instance.OnDialogueEnd?.Invoke(null);
            dialogueGroup.alpha = 0;
            answerGroup.alpha = 0;
            dialogueCursor.gameObject.SetActive(false);
            dialogueText.text = "";
            canvasTweener = null;
        }

        foreach(var btn in instantiatedButtons)
        {
            Destroy(btn.gameObject);
        }
        instantiatedButtons = new List<DialogueButton>();
    }

    public void Quit()
    {
        uiCo = StartCoroutine(DoQuit());
    }

    private IEnumerator DoQuit()
    {
        canvasTweener = dialogueGroup.DOFade(0, .5f);
        yield return new WaitForSeconds(.8f);
        dialogueGroup.alpha = 0;
        answerGroup.alpha = 0;
        dialogueCursor.gameObject.SetActive(false);
        dialogueText.text = "";
    }

    private IEnumerator DoInfiniteBounce()
    {
        while (true) {
            cursorTweener = dialogueCursor.DOLocalMoveY(dialogueCursor.localPosition.y + 5,1.2f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1.5f);
            cursorTweener = dialogueCursor.DOLocalMoveY(dialogueCursor.localPosition.y - 5, 1.2f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
