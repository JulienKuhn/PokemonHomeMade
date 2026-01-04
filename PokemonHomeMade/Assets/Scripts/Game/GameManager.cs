using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameSave currentSave = new GameSave();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void LoadSaveByID(int id)
    {
        if (!PlayerPrefs.HasKey($"Save{id}"))
        {
            Debug.LogError($"Save{id} doesnt exist");
            return;
        }
        string jsonData = PlayerPrefs.GetString($"Save{id}");
        currentSave = JsonUtility.FromJson<GameSave>(jsonData);

        SceneManager.LoadScene("SampleScene");
    }

    public void Reload()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Save() 
    { 
        string jsonData = JsonUtility.ToJson(currentSave);
        PlayerPrefs.SetString($"Save{currentSave.SaveID}", jsonData);
    }

    public GameSave? GetGameSaveByID(int id)
    {
        if (!PlayerPrefs.HasKey($"Save{id}"))
        {
            Debug.Log($"Save{id} doesnt exist");
            return null;
        }
        string jsonData = PlayerPrefs.GetString($"Save{id}");
        return JsonUtility.FromJson<GameSave>(jsonData);
    }
}
