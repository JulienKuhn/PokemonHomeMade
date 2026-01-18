using System;
using System.Collections.Generic;
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
        movement.CanMove = false;
    }

    public void UnFreeze()
    {
        movement.CanMove = true;
    }

    public void MovePlayerToLocations(List<Vector3> pos, Action callback)
    {
        movement.MoveToLocations(pos, callback);
    }
}
