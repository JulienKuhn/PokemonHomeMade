using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PokedexPokemonInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Image PokemonImage;
    [SerializeField] private TextMeshProUGUI cursor;
    [SerializeField] private TextMeshProUGUI PokemonName;
    [SerializeField] private TextMeshProUGUI LifeAmount, PPAmount;
    [SerializeField] private Image LifeFiller, PPFiller;

    public Action OnClick;
    private Tweener tweener;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(() => this.OnClick?.Invoke());
        cursor.alpha = 0;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tweener != null && tweener.IsPlaying())
            tweener.Kill();

        //tweener = this.transform.DOScale(Vector3.one * 1.1f, .8f);
        cursor.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tweener != null && tweener.IsPlaying())
            tweener.Kill();

        //tweener = this.transform.DOScale(Vector3.one, .8f);
        cursor.alpha = 0;
    }
}
