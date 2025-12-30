using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("LogoIntro")]
    [SerializeField] private RectTransform ScrollPaper;
    [SerializeField] private RectTransform ScrollPaperStartPos, ScrollPaperEndPos;
    [SerializeField] private Image HomemadeLogoImg;
    [SerializeField] private RectTransform HomemadeLogoEndPos;
    [SerializeField] private TextMeshProUGUI ClickText;
    [SerializeField] private CanvasGroup LogoIntroPanel;
    private bool canClick = false, hasClicked = false;

    [Header("SelectionPanel")]
    [SerializeField] private CanvasGroup SelectionPanel;
    [SerializeField] private MenuCardController SaveCard1, SaveCard2, SaveCard3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveCard1.ShowContinue("Evan Gigachad - Kanto \n 30/12/2025 23:09");
        SaveCard2.ShowNewGame();
        SaveCard3.ShowNewGame();
        StartCoroutine(DoIntro()); 
    }

    private void Update()
    {        
        var mouse = Mouse.current;
        if (mouse == null) return;

        // 1. Touche '²' (Backquote) pour ouvrir/fermer
        if (canClick && (mouse.leftButton.isPressed || mouse.rightButton.isPressed))
        {
            hasClicked = true;
        }
    }

    private IEnumerator DoIntro()
    {
        yield return new WaitForSeconds(1.5f);
        LogoIntroPanel.DOFade(1, .8f);
        yield return new WaitForSeconds(.9f);
        ScrollPaper.DOScale(Vector3.one, 1.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(2.5f);
        ScrollPaper.DOMove(ScrollPaperStartPos.position, 1.5f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(1.52f);
        HomemadeLogoImg.DOFillAmount(1, 1.1f).SetEase(Ease.Linear);
        ScrollPaper.DOMove(ScrollPaperEndPos.position, 1.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1.52f);
        HomemadeLogoImg.rectTransform.DOMove(HomemadeLogoEndPos.position,1.5f).SetEase(Ease.Linear);
        HomemadeLogoImg.rectTransform.DOScale(Vector3.one * 0.7f, 1.5f);
        yield return new WaitForSeconds(1.52f);
        ClickText.DOFade(1, 1);
        yield return new WaitForSeconds(1.2f);
        canClick = true;
        hasClicked = false;
        while (!hasClicked) {
            ClickText.DOFontSize(60, 1.5f);
            yield return new WaitForSeconds(1.52f);
            ClickText.DOFontSize(50, 1.5f);
            yield return new WaitForSeconds(1.52f);
        }
        LogoIntroPanel.DOFade(0, .8f);
        yield return new WaitForSeconds(.9f);
        SelectionPanel.DOFade(1, .8f);
        yield return new WaitForSeconds(.9f);
        SelectionPanel.interactable = true;
    }
}
