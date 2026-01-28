using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static CustomEnums;

public class MoveButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI moveText;
    [SerializeField] private TextMeshProUGUI PPText;
    [SerializeField] private Image border;
    [SerializeField] private Image typeIndicator;
    [SerializeField] private TextMeshProUGUI typeIndicatorText;

    public Action OnClick;
    private Tweener tweener;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(() => this.OnClick?.Invoke());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tweener != null && tweener.IsPlaying())
            tweener.Kill();

        tweener = this.transform.DOScale(Vector3.one * 1.1f, .8f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tweener != null && tweener.IsPlaying())
            tweener.Kill();

        tweener = this.transform.DOScale(Vector3.one, .8f);
    }

    public void Setup(string btnText, string ppText, PokemonType type)
    {

        button.interactable = true;
        moveText.text = btnText;
        PPText.text = ppText;
        Color color = ColorManager.instance.GetColorByType(type);
        border.color = color;

        typeIndicator.gameObject.SetActive(type != PokemonType.None);
        if (type != PokemonType.None) 
        { 
            typeIndicator.color = color;
            typeIndicatorText.text = PokemonStringifier.GetTypeAcronym(type);
        }
    }

    public void Disable()
    {
        button.interactable = false;

        moveText.text = "";
        PPText.text = "";
        Color color = ColorManager.instance.GetColorByType(PokemonType.None);
        border.color = color;

        typeIndicator.gameObject.SetActive(false);
    }

    public void UpdatePPText(string ppText)
    {
        PPText.text = ppText;
    }
}
