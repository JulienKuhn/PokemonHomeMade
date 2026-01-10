using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;

    public void StopMovement()
    {
        movement.StopMovements();
    }
}
