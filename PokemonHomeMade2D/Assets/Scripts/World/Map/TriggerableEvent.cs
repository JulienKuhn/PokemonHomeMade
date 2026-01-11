using Unity.VisualScripting;
using UnityEngine;

public class TriggerableEvent : MonoBehaviour
{
    public enum EventType
    {
        ChangeMap,
        Dialogue,
    }

    [SerializeField] private MapController controller;
    [SerializeField] private EventType type;
    [SerializeField] private string[] conditions;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && controller.hasStarted)
        {
            GameManager.instance.TriggerCustomEvent(type, conditions);
        }
    }
}
