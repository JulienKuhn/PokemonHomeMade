using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkToObjective", menuName = "Quest/Objective/TalkToObjective")]
public class TalkToObjective : QuestObjective
{
    public int RelatedMapID;
    public Vector3 Location;
    public Vector3 Size;

    private RaycastableObject ObjectiveDialogue = null;

    public override void StartObjective()
    {
        MapManager.instance.OnMapLoaded += this.OnMapLoaded;
    }
    public void OnMapLoaded(int mapId)
    {
        if (mapId == RelatedMapID)
        {
            ObjectiveDialogue = Instantiate(QuestManager.instance.RaycastableObject);
            ObjectiveDialogue.transform.localScale = Size;
            ObjectiveDialogue.transform.position = Location;
            ObjectiveDialogue.OnInteractionDone += this.OnInteractionDone;
        }
        else if (ObjectiveDialogue != null)
        {
            ObjectiveDialogue.OnInteractionDone = null;
            Destroy(ObjectiveDialogue.gameObject);
            ObjectiveDialogue = null;
        }
    }

    private void OnInteractionDone()
    {
        ObjectiveDialogue.StartCoroutine(DoDialogue());
    }

    private IEnumerator DoDialogue()
    {
        Debug.Log("Bonjour");
        yield return new WaitForSeconds(.5f);
        Debug.Log("ça va ?");
        yield return new WaitForSeconds(.5f);
        Debug.Log("Labise");
        yield return new WaitForSeconds(.5f);
        Debug.Log("bye");
        yield return new WaitForSeconds(.5f);
        ObjectiveDialogue.OnInteractionDone = null;
        Destroy(ObjectiveDialogue.gameObject);
        ObjectiveDialogue = null;
        OnObjectiveComplete?.Invoke();
    }
}
