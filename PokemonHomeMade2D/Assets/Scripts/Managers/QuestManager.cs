using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [SerializeField] private QuestData questData;

    private QuestData workingQuest;

    [Header("Instantiables")]
    public GotoDetector GotoDetector;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questData.StartQuest();
    }
}
