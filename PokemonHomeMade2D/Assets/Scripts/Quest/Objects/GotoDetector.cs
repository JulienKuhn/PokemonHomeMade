using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GotoDetector : MonoBehaviour
{
    public Action OnTriggerActivated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerActivated?.Invoke();
        }
    }
}
