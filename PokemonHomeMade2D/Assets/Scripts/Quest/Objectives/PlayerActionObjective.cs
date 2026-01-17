using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerActionObjective", menuName = "Quest/Objective/PlayerAction")]
public class PlayerActionObjective : QuestObjective
{
    public enum ActionType
    {
        GiveItem,
        GiveCurrency,
        Freeze,
        UnFreeze,
        FullHeal,
    }

    public ActionType Action;

    public override void StartObjective()
    {
        switch (Action)
        {
            case ActionType.GiveItem:
                Debug.LogError("GiveItem not implemented");
                OnObjectiveComplete?.Invoke();
                break;
            case ActionType.GiveCurrency:
                Debug.LogError("GiveItem not implemented");
                OnObjectiveComplete?.Invoke();
                break;
            case ActionType.Freeze:
                GameManager.instance.FreezePlayer();
                OnObjectiveComplete?.Invoke();
                break;
            case ActionType.UnFreeze:
                GameManager.instance.UnFreezePlayer();
                OnObjectiveComplete?.Invoke();
                break;
            case ActionType.FullHeal:
                Debug.LogError("GiveItem not implemented");
                OnObjectiveComplete?.Invoke();
                break;
        }
    }
}
