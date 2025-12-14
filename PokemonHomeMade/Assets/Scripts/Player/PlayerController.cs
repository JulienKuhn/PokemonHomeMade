using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 currentTile;
    [SerializeField] private SpriteAnimator animator;

    public InputAction moveAction;
    public int frameRate = 60;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;
    }

    private void OnEnable()
    {
        moveAction.Enable(); 
        moveAction.performed += OnMovePerformed;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        moveAction.performed -= OnMovePerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        Console.Write(input.ToString());

        if (input.x > 0)
        {
            animator.SetReference(SpriteAnimator.FacingDirection.Front);
            MoveTo(new Vector2(1, 0));
        }
        else if (input.x < 0)
        {
            animator.SetReference(SpriteAnimator.FacingDirection.Back);
            MoveTo(new Vector2(-1, 0));
        }
        else if (input.y > 0)
        {
            animator.SetReference(SpriteAnimator.FacingDirection.Left);
            MoveTo(new Vector2(0, -1));
        }
        else if (input.y < 0)
        {
            animator.SetReference(SpriteAnimator.FacingDirection.Right);
            MoveTo(new Vector2(0, 1));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void MoveTo(Vector2 relativeMovement)
    {
        Vector2 nextTile = currentTile + relativeMovement;

        TileController destinationTile = TileManager.instance.GetTileByCoords(nextTile);

        if (destinationTile && destinationTile.isWalkable)
        { 
            transform.position = destinationTile.transform.position;
            currentTile = nextTile;
        }
    }
}
