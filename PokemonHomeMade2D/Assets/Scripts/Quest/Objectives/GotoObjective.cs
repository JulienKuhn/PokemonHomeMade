using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GotoObjective", menuName = "Quest/Objective/GotoObjective")]
public class GotoObjective : QuestObjective
{
    public int RelatedMapID;
    public Vector3 Location;
    public Vector3 Size;

    private GotoDetector ObjectiveDetector = null;

    public override void StartObjective()
    {
        MapManager.instance.OnMapLoaded += this.OnMapLoaded;
    }

    public void OnMapLoaded(int mapId)
    {
        if(mapId == RelatedMapID)
        {
            ObjectiveDetector = Instantiate(QuestManager.instance.GotoDetector);
            ObjectiveDetector.transform.localScale = Size;
            ObjectiveDetector.transform.position = Location;
            ObjectiveDetector.OnTriggerActivated += this.OnDestinationReached;
        }
        else if(ObjectiveDetector != null)
        {
            ObjectiveDetector.OnTriggerActivated = null;
            Destroy(ObjectiveDetector.gameObject);
            ObjectiveDetector = null;
        }
    }

    private void OnDestinationReached()
    {
        OnObjectiveComplete?.Invoke();

        if (ObjectiveDetector != null)
        {
            ObjectiveDetector.OnTriggerActivated = null;
            Destroy(ObjectiveDetector.gameObject);
            ObjectiveDetector = null;
        }
    }
}
