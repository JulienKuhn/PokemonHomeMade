using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastableObject : MonoBehaviour
{
    public Action OnInteractionDone;
    [SerializeField] private GameObject InteractionBubble;
    Tweener tweener = null;
    Coroutine coroutine = null;

    public void OnRaycastStart()
    {
        if (InteractionBubble.activeSelf) return;

        if (tweener != null)
            tweener.Kill();

        if (coroutine != null)
            StopCoroutine(coroutine);

        InteractionBubble.SetActive(true);
        StartCoroutine(DoBubbleAnimation());
    }

    public void OnRaycastEnd()
    {
        if(tweener != null)
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
        InteractionBubble.transform.localPosition = new Vector3(0,180,0);
        while (InteractionBubble.activeSelf)
        {
            tweener = InteractionBubble.transform.DOLocalMoveY(200, .9f).SetEase(Ease.InOutQuart);
            yield return new WaitForSeconds(1);
            tweener = InteractionBubble.transform.DOLocalMoveY(180, .9f).SetEase(Ease.InOutQuart);
            yield return new WaitForSeconds(1);
        }
    }
}
