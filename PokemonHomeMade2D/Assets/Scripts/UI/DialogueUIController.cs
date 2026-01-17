using DG.Tweening;
using System;
using System.Collections;
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
    [SerializeField] private Button answerBTNPrefab;

    private DialogueData currentDialogue = null;
    private Tweener tweener;

    private void Start()
    {
        dialogueGroup.alpha = 0;
        answerGroup.alpha = 0;
        dialogueCursor.gameObject.SetActive(false);
        dialogueText.text = "";
    }

    public void StartDialogue(DialogueData data)
    {
        currentDialogue = data;
        StartCoroutine(DoDialogue());
    }

    private IEnumerator DoDialogue()
    {
        dialogueGroup.DOFade(1, .5f);
        yield return new WaitForSeconds(.8f);
        foreach (var line in currentDialogue.Lines)
        {
            speakerText.text = line.SpeakerName;
            dialogueText.text = "";
            dialogueCursor.gameObject.SetActive(false);
            yield return null;
            float duration = line.Line.Length * 0.03f;
            dialogueText.DOText(line.Line, duration);
            yield return new WaitForSeconds(duration + .1f);
            dialogueCursor.gameObject.SetActive(true);
            Coroutine couroutine = StartCoroutine(DoInfiniteBounce());
            bool hasPlayerClicked = false;

            while (!hasPlayerClicked)
            {
                var mouse = Mouse.current;

                if (mouse != null && (mouse.leftButton.wasPressedThisFrame || mouse.rightButton.wasPressedThisFrame))
                {
                    hasPlayerClicked = true;
                }
                yield return null;
            }

            if (tweener != null)
                tweener.Kill();

            StopCoroutine(couroutine);
        }

        dialogueGroup.DOFade(0, .5f);
        yield return new WaitForSeconds(.8f);
        dialogueGroup.alpha = 0;
        answerGroup.alpha = 0;
        dialogueCursor.gameObject.SetActive(false);
        dialogueText.text = "";
        DialogueManager.instance.OnDialogueEnd?.Invoke();
    }

    private IEnumerator DoInfiniteBounce()
    {
        while (true) {
            tweener = dialogueCursor.DOLocalMoveY(dialogueCursor.localPosition.y + 5,1.2f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1.5f);
            tweener = dialogueCursor.DOLocalMoveY(dialogueCursor.localPosition.y - 5, 1.2f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
