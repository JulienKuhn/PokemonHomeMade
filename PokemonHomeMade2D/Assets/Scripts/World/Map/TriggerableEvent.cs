using Unity.VisualScripting;
using UnityEngine;

public class TriggerableEvent : MonoBehaviour
{
    public enum EventType
    {
        ChangeMap,
        Dialogue,
    }

    [SerializeField] private EventType type;
    [SerializeField] private string[] conditions;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger !");
        if (other.CompareTag("Player"))
        {
            GameManager.instance.TriggerCustomEvent(type, conditions);
        }
    }
}
