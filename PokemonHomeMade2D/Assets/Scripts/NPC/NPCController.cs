using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCController : MonoBehaviour
{
    public int NPCID;
    public Animator NPCAnimator;
    public bool CanMove = true;
    [SerializeField] private RaycastableObject raycastableObject;
    [SerializeField] private SpriteRenderer sprite;


    [Header("Walk Settings")]
    [SerializeField] private List<Transform> walkSpot;
    [SerializeField] private bool isLoopingPathway;
    [SerializeField] private float durationPerUnit;
    [SerializeField] private Vector2 waitingDurationSpan;

    [Header("UI")]
    [SerializeField] private Transform ExclamationMark;

    private int currentSpot = 0;
    private Coroutine walkingCo;
    private Tweener walkingTweener;
    private bool isWalking = false;
    private bool isVisible = true;

    public Action OnNPCInteracted;
    public Action OnMovePerformed;

    private void Start()
    {
        if (NPCManager.instance.GetNPCVisibility(NPCID))
            ShowNPC();
        else
            HideNPC();


        NPCManager.instance.OnNPCStatusChanged += this.OnNPCStatusChanged;
    }

    private void OnNPCStatusChanged(int id, bool show)
    {
        if (this.NPCID == id)
        {
            if (show)
                ShowNPC();
            else 
                HideNPC();
        }
    }

    public void ShowNPC()
    {
        if (isVisible) return;
        Debug.Log(" show ");

        sprite.DOFade(1, .5f);
        GetComponent<BoxCollider2D>().enabled = true;
        raycastableObject.OnInteractionDone += this.OnInteractionDone;
        if (walkSpot.Count > 0)
            walkingCo = StartCoroutine(DoWalking());

        isVisible = true;
    }

    public void HideNPC()
    {
        if (!isVisible) return;

        sprite.DOFade(0, .5f);
        GetComponent<BoxCollider2D>().enabled = false;

        isVisible = false;
    }

    private IEnumerator DoWalking()
    {
        isWalking = true;
        while (CanMove)
        {
            if (isLoopingPathway)
            {
                currentSpot++;
                if(currentSpot >= walkSpot.Count) currentSpot = 0;
            }
            else
            {
                currentSpot = (int)UnityEngine.Random.Range(0, walkSpot.Count);
            }

            float distance = Vector3.Distance(transform.position, walkSpot[currentSpot].position);
            walkingTweener = transform.DOMove(walkSpot[currentSpot].position, distance * durationPerUnit);
            yield return new WaitForSeconds(distance * durationPerUnit);

            // Add Face direction in animator

            float waitingTime = UnityEngine.Random.Range(waitingDurationSpan.x, waitingDurationSpan.y);
            yield return new WaitForSeconds(waitingTime);
            yield return null;
        }
    }

    public void Move(List<Vector3> points, float timeTravelPerUnit, Ease ease)
    {
        StartCoroutine(DoMove(points, timeTravelPerUnit, ease));
    }

    private IEnumerator DoMove(List<Vector3> points, float timeTravelPerUnit, Ease ease)
    {
        isWalking = true;

        foreach (Vector3 point in points) 
        {
            float distance = Vector3.Distance(transform.position, point);
            walkingTweener = transform.DOMove(point, distance * timeTravelPerUnit).SetEase(ease);
            yield return new WaitForSeconds(distance * timeTravelPerUnit);
        }
        yield return null;
        isWalking = false;

        OnMovePerformed?.Invoke();
    }

    public void Freeze()
    {
        if (!isWalking) return;

        if(walkingCo != null) 
            StopCoroutine(walkingCo);

        if(walkingTweener != null)
            walkingTweener.Kill();

        currentSpot--;
        CanMove = false;
    }

    public void UnFreeze()
    {
        CanMove = true;
        walkingCo = StartCoroutine(DoWalking());
    }

    public void DoExclamationMark()
    {
        StartCoroutine(DoExclamationMarkAnimation());
    }

    public IEnumerator DoExclamationMarkAnimation()
    {
        ExclamationMark.localScale = Vector3.zero;
        ExclamationMark.DOScale(Vector3.one, .65f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(.7f);
        ExclamationMark.DOScale(Vector3.zero, .4f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(.6f);
        OnMovePerformed?.Invoke();
    }

    private void OnInteractionDone()
    {
        OnNPCInteracted?.Invoke();

        if (isWalking)
            Freeze();
    }

    public void SetInteractable(bool interactable)
    {
        if (interactable)
            raycastableObject.OnInteractionDone += () => this.OnNPCInteracted?.Invoke();
        else
            raycastableObject.OnInteractionDone = null;
    }
}
