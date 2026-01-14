using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Obligatoire pour le nouveau système

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask obstacleLayer;

    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    public Vector3 footOffset = new Vector3(0f, -0.4f, 0f);
    private Coroutine movementCoroutine = null;
    private int frameBuffer = 15;
    private int frameCount = 0;
    public bool CanMove = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving && CanMove)
        {
            // Lecture des touches via le nouveau système
            var keyboard = Keyboard.current;
            if (keyboard == null) return;

            Vector2 movementInput = Vector2.zero;

            // Gestion ZQSD ou Flèches
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) movementInput.y = 1;
            else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) movementInput.y = -1;
            else if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) movementInput.x = -1;
            else if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) movementInput.x = 1;
            else frameCount = 0;

            if (movementInput != Vector2.zero)
            {
                // On met à jour l'Animator UNIQUEMENT quand on bouge
                // Cela permet de garder la dernière direction quand on s'arrête
                animator.SetFloat("moveX", movementInput.x);
                animator.SetFloat("moveY", movementInput.y);

                var targetPos = transform.position + new Vector3(movementInput.x, movementInput.y, 0);

                if (IsWalkable(targetPos))
                {
                    if (frameCount >= frameBuffer)
                    {
                        movementCoroutine = StartCoroutine(Move(targetPos));
                    }
                    else
                    {
                        frameCount++;
                    }
                }
            }
        }

        // On informe l'animator de l'état du mouvement
        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
        movementCoroutine = null;
    }

    public void StopMovements()
    {
        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        isMoving = false;
        frameCount = 0;
        animator.SetBool("isMoving", isMoving);
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        // On ajoute l'offset à la position cible
        // Ainsi, on vérifie si les PIEDS peuvent aller sur la case suivante
        Vector3 checkPos = targetPos + footOffset;

        if (Physics2D.OverlapCircle(checkPos, 0.15f, obstacleLayer) != null)
        {
            return false;
        }
        return true;
    }
}