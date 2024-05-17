using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(string currentLevel)
    {
        PlayerPrefs.SetString("CurrentLevel", currentLevel);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (HasSaveData())
        {
            string currentLevel = PlayerPrefs.GetString("CurrentLevel");
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentLevel);
        }
        else
        {
            Debug.LogWarning("No save data found!");
        }
    }

    public bool HasSaveData()
    {
        return PlayerPrefs.HasKey("CurrentLevel");
    }
}
