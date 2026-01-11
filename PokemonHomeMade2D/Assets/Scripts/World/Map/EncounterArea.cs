using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EncounterArea : MonoBehaviour
{
    public Action OnAreaEnter;
    [SerializeField] private GameObject overArea;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            overArea.SetActive(true);
            OnAreaEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            overArea.SetActive(false);
        }
    }
}
