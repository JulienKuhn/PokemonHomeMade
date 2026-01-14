using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;

    public void StopMovement()
    {
        movement.StopMovements();
    }
    public void Freeze()
    {
        movement.CanMove = true;
    }

    public void UnFreeze()
    {
        movement.CanMove = true;
    }
}
