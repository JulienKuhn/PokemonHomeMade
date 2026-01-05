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
    [SerializeField] private Image animatedImage;
    public bool isAnimatating = false;
    private Coroutine animCo;

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
        animatedImage.sprite = spriteReferences[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetReference(FacingDirection newDirection, float movementSpeed)
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

        animCo=StartCoroutine(PerformMove(movementSpeed));
    }

    private IEnumerator PerformMove(float movementSpeed)
    {
        isAnimatating = true;
        int currentSprite = (int)spriteRange.x;
        while (isAnimatating)
        {
            currentSprite = (int)spriteRange.x;
            while (currentSprite <= (int)spriteRange.y)
            {
                animatedImage.sprite = spriteReferences[currentSprite];
                currentSprite++;
                yield return new WaitForSeconds(movementSpeed/4.0f);
            }
        }
        animatedImage.sprite = spriteReferences[(int)spriteRange.x];
    }
}