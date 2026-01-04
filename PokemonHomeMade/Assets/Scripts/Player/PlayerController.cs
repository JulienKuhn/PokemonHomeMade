using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 currentTile;
    [SerializeField] private SpriteAnimator animator;

    public InputAction moveAction;
    public int frameRate = 60;
    public float movementSpeed = .3f;
    private bool canMove = true;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (canMove && keyboard.upArrowKey.isPressed)
        {
            animator.SetReference(SpriteAnimator.FacingDirection.Back);
            StartCoroutine(MoveTo(new Vector2(0, -1)));
        } 
        else if (canMove && keyboard.downArrowKey.isPressed)
        {
            animator.SetReference(SpriteAnimator.FacingDirection.Front);
            StartCoroutine(MoveTo(new Vector2(0, 1)));
        }
        else if (canMove && keyboard.leftArrowKey.isPressed)
        {
            animator.SetReference(SpriteAnimator.FacingDirection.Left);
            StartCoroutine(MoveTo(new Vector2(-1, 0)));
        }
        else if (canMove && keyboard.rightArrowKey.isPressed)
        {
            animator.SetReference(SpriteAnimator.FacingDirection.Right);
            StartCoroutine(MoveTo(new Vector2(1, 0)));
        }
    }

    private IEnumerator MoveTo(Vector2 relativeMovement)
    {
        canMove = false;
        Vector2 nextTile = currentTile + relativeMovement;

        TileController destinationTile = TileManager.instance.GetTileByCoords(nextTile);

        if (destinationTile && destinationTile.isWalkable)
        { 
            transform.DOMove(destinationTile.transform.position, movementSpeed).SetEase(Ease.Linear);
            currentTile = nextTile;
            yield return new WaitForSeconds(movementSpeed);
        }
        canMove = true;
    }
}
