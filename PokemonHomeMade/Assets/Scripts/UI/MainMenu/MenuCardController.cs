using DG.Tweening;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuCardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private RectTransform NewGameContainer;
    [SerializeField] private Outline NewGameOutline; // Utiliser CanvasGroup pour le Fade est souvent plus simple
    [SerializeField] private RectTransform ContinueContainer;
    [SerializeField] private Outline ContinueOutline;
    [SerializeField] private TextMeshProUGUI ContinueText;
    private bool isNewGame = true;
    public Action OnClick;
    // Cette méthode remplace ton AddListener dans le Start

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isNewGame)
        {
            // .Kill() permet d'arrêter l'animation précédente si on survole très vite
            NewGameContainer.DOScale(1.2f, 0.5f).SetEase(Ease.OutBack);
            NewGameOutline.DOFade(1, 0.5f);
        }
        else
        {
            ContinueContainer.DOScale(1.2f, 0.5f).SetEase(Ease.OutBack);
            ContinueOutline.DOFade(1, 0.5f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isNewGame)
        {
            NewGameContainer.DOScale(1.0f, 0.5f);
            NewGameOutline.DOFade(0, 0.5f);
        }
        else
        {
            ContinueContainer.DOScale(1.0f, 0.5f);
            ContinueOutline.DOFade(0, 0.5f);
        }
    }

    public void ShowNewGame() 
    {     
        NewGameContainer.gameObject.SetActive(true); 
        ContinueContainer.gameObject.SetActive(false);
        isNewGame = true;
    }
    public void ShowContinue(string SaveName) 
    {
        ContinueText.text = SaveName;
        NewGameContainer.gameObject.SetActive(false);
        ContinueContainer.gameObject.SetActive(true);
        isNewGame = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
    }
}
