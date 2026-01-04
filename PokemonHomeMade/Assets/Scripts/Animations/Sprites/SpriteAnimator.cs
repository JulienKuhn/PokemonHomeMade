using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteReferences;
    [SerializeField] private float animationDelayInSeconds;

    public Vector2 spriteRange = new Vector2(0,4);
    private Image animatedImage;
    public bool isAnimatating = false;
    private Coroutine animCo;
    private Tweener animTweener;

    public enum FacingDirection
    {
        Front,
        Back,
        Left,
        Right,        
    }
    public FacingDirection currentFacingDirection = FacingDirection.Front;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatedImage = GetComponent<Image>();
        SetReference(currentFacingDirection);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetReference(FacingDirection newDirection)
    {
        currentFacingDirection = newDirection;

        switch (newDirection)
        {
            case FacingDirection.Front:
                spriteRange = new Vector2(0, 3);
                break;
            case FacingDirection.Back:
                spriteRange = new Vector2(12, 15);
                break;
            case FacingDirection.Left:
                spriteRange = new Vector2(4, 7);
                break;
            case FacingDirection.Right:
                spriteRange = new Vector2(8, 11);
                break;
        }

        if (animCo != null)
            StopCoroutine(animCo);

        animCo=StartCoroutine(PerformMove());
    }

    private IEnumerator PerformMove()
    {
        isAnimatating = true;
        int currentSprite = (int)spriteRange.x;
        while(currentSprite != (int)spriteRange.y)
        {
            animatedImage.sprite = spriteReferences[currentSprite];
            currentSprite++;
            yield return new WaitForSeconds(animationDelayInSeconds);
        }
        isAnimatating = false;
    }
}