using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WaitObjective", menuName = "Quest/Objective/WaitObjective")]
public class WaitObjective : QuestObjective
{
    [SerializeField] private float WaitingTime;

    public override void StartObjective(QuestData relatedQuest)
    {
        GameManager.instance.StartCoroutine(this.DoWait());
    }

    private IEnumerator DoWait()
    {
        yield return new WaitForSeconds(WaitingTime);
        this.OnObjectiveComplete?.Invoke();
    }

}
