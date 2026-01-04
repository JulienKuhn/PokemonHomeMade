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
    private bool canSkipIntro = false;
    private Coroutine IntroCo;

    [Header("SelectionPanel")]
    [SerializeField] private CanvasGroup SelectionPanel;
    [SerializeField] private MenuCardController SaveCard1, SaveCard2, SaveCard3;

    [Header("New Game Panel")]
    [SerializeField] private CanvasGroup NewGamePanel;
    [SerializeField] private NewGameController newGameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupCards();
        IntroCo =StartCoroutine(DoIntro()); 
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
        if (canSkipIntro && (mouse.leftButton.isPressed || mouse.rightButton.isPressed))
        {
            canSkipIntro = false;
            canClick = false;
            StopCoroutine(IntroCo);
            LogoIntroPanel.DOFade(0, 0f);
            SelectionPanel.DOFade(1, 1.2f).OnComplete(() =>
            {
                SelectionPanel.interactable = true;
                SelectionPanel.blocksRaycasts = true;
            });
        }
    }

    private void SetupCards()
    {
        GameSave Save01 = GameManager.Instance.GetGameSaveByID(1);
        GameSave Save02 = GameManager.Instance.GetGameSaveByID(2);
        GameSave Save03 = GameManager.Instance.GetGameSaveByID(3);

        if (Save01 != null)
        {
            SaveCard1.ShowContinue($"{Save01.PlayerName} - {Save01.PlayerLocation} \n {Save01.LastSavedTime}");
            SaveCard1.OnClick += () => GameManager.Instance.LoadSaveByID(1);
        }
        else
        {
            SaveCard1.ShowNewGame();
            SaveCard1.OnClick += () => OnNewSaveSelected(1);
        }

        if (Save02 != null)
        { 
            SaveCard2.ShowContinue($"{Save02.PlayerName} - {Save02.PlayerLocation} \n {Save02.LastSavedTime}");
            SaveCard2.OnClick += () => GameManager.Instance.LoadSaveByID(2);
        }
        else
        {
            SaveCard2.ShowNewGame();
            SaveCard2.OnClick += () => OnNewSaveSelected(2);
        }

        if (Save03 != null)
        { 
            SaveCard3.ShowContinue($"{Save03.PlayerName} - {Save03.PlayerLocation} \n {Save03.LastSavedTime}");
            SaveCard3.OnClick += () => GameManager.Instance.LoadSaveByID(3);
        }
        else
        {
            SaveCard3.ShowNewGame();
            SaveCard3.OnClick += () => OnNewSaveSelected(3);
        }
    }

    private IEnumerator DoIntro()
    {
        yield return new WaitForSeconds(1.5f);
        canSkipIntro = true;
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
        yield return new WaitForSeconds(.2f);
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
        canSkipIntro = false;
        SelectionPanel.interactable = true;
        SelectionPanel.blocksRaycasts = true;
    }

    private void OnNewSaveSelected(int saveID)
    {
        GameManager.Instance.currentSave.SaveID = saveID;
        NewGamePanel.DOFade(1, .5f);
        SelectionPanel.interactable = false;
        NewGamePanel.interactable = true;
        NewGamePanel.blocksRaycasts = true;
    }
}
