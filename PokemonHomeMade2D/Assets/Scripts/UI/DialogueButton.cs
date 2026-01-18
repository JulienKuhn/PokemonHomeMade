using DG.Tweening;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI btnText;
    [SerializeField] private CanvasGroup btnGroup;

    private Tweener tweener = null;
    public Action<int> OnButtonClicked;

    public void Setup(int id, string text)
    {
        btnText.text = text;
        btn.onClick.AddListener(() => OnButtonClicked(id));
        btnGroup.alpha = 1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(tweener !=null && !tweener.IsComplete())
        {
            tweener.Kill();
        }
        tweener = btn.image.DOFade(1, .3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tweener != null && !tweener.IsComplete())
        {
            tweener.Kill();
        }
        tweener = btn.image.DOFade(0, .3f);
    }
}
