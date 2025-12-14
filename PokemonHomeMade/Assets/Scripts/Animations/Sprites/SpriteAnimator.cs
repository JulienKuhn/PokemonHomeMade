using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteReferences;
    [SerializeField] private int frameTransitionDelay;

    private int currentFrame = 0;
    private int currentSprite = 0;
    public Vector2 frameRange = new Vector2(0,4);
    private Image animatedImage;
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

        if (currentFrame >= frameTransitionDelay)
        {
            currentFrame = 0;

            if (currentSprite >= (int)frameRange.y)
                currentSprite = (int)frameRange.x;

            animatedImage.sprite = spriteReferences[currentSprite];
            currentSprite++;
        }
        else
        {
            currentFrame++;
        }

    }

    public void SetReference(FacingDirection newDirection)
    {
        currentFacingDirection = newDirection;

        switch (newDirection)
        {
            case FacingDirection.Front:
                frameRange = new Vector2(0, 4);
                break;
            case FacingDirection.Back:
                frameRange = new Vector2(5, 9);
                break;
            case FacingDirection.Left:
                frameRange = new Vector2(10, 14);
                break;
            case FacingDirection.Right:
                frameRange = new Vector2(15, 19);
                break;
        }

    }
}