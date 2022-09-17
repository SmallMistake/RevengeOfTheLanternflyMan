using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void Retry()
    {
        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        levelLoader.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
