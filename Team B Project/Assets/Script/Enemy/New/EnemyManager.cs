using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    private List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Clear the list of enemies when a new scene is loaded
        enemies.Clear();
    }

    public void RegisterEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        CheckAllEnemiesDefeated();
    }

    private void CheckAllEnemiesDefeated()
    {
        if (enemies.Count == 0)
        {
            OnAllEnemiesDefeated();
        }
    }

    private void OnAllEnemiesDefeated()
    {
        Debug.Log("All enemies defeated!");
        // Trigger any global event here
    }

    public int GetRemainingEnemies()
    {
        return enemies.Count;
    }
}
