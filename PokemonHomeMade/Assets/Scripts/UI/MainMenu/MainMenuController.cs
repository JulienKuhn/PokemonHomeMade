using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [Header("LogoIntro")]
    [SerializeField] private RectTransform ScrollPaper;
    [SerializeField] private RectTransform ScrollPaperStartPos, ScrollPaperEndPos;
    [SerializeField] private Image HomemadeLogoImg;
    [SerializeField] private RectTransform HomemadeLogoEndPos;
    [SerializeField] private TextMeshProUGUI ClickText;
    [SerializeField] private Image Blackscreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DoIntro()); 
    }

    private IEnumerator DoIntro()
    {
        yield return new WaitForSeconds(1.5f);
        Blackscreen.DOFade(0, .8f);
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
        while (true) {
            ClickText.DOFontSize(60, 1.5f);
            yield return new WaitForSeconds(1.52f);
            ClickText.DOFontSize(50, 1.5f);
            yield return new WaitForSeconds(1.52f);
        }
    }
}
