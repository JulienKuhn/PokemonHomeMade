using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastableObject : MonoBehaviour
{
    public Action OnInteractionDone;
    [SerializeField] private GameObject InteractionBubble;
    [SerializeField] private Transform topSpot;
    [SerializeField] private Transform bottomSpot;
    Tweener tweener = null;
    Coroutine coroutine = null;

    public bool isInteractable;

    public void OnRaycastStart()
    {
        if (InteractionBubble.activeSelf || !isInteractable) return;

        if (tweener != null)
            tweener.Kill();

        if (coroutine != null)
            StopCoroutine(coroutine);

        InteractionBubble.SetActive(true);
        StartCoroutine(DoBubbleAnimation());
    }

    public void OnRaycastEnd()
    {
        if (!isInteractable) return;
        if (tweener != null)
            tweener.Kill();

        if(coroutine != null)
            StopCoroutine(coroutine);

        InteractionBubble.SetActive(false);
    }

    private void Update()
    {
        if (InteractionBubble.activeSelf)
        {
            var keyboard = Keyboard.current;
            if (keyboard == null) return;

            if (keyboard.eKey.wasPressedThisFrame) {
                OnInteractionDone?.Invoke();
            }
        }
    }

    private IEnumerator DoBubbleAnimation()
    {
        while (InteractionBubble.activeSelf)
        {
            tweener = InteractionBubble.transform.DOMove(topSpot.position, .9f).SetEase(Ease.InOutQuart);
            yield return new WaitForSeconds(1);
            tweener = InteractionBubble.transform.DOMove(bottomSpot.position, .9f).SetEase(Ease.InOutQuart);
            yield return new WaitForSeconds(1);
        }
    }
}
