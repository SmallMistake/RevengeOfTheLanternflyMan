using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator levelTransitionAnimator;
    public void Retry()
    {
        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        levelLoader.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string levelName)
    {
        SaveSystemGameObject saveSystem = FindObjectOfType<SaveSystemGameObject>();
        if (saveSystem)
        {
            saveSystem.SavePlayer();
        }
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1;
    }

    public void EndLevelWithTransition(string levelName)
    {
        Time.timeScale = 0;
        levelTransitionAnimator.SetTrigger("StartTransition");
        StartCoroutine(LoadWithOffset(levelName));
    }

    IEnumerator LoadWithOffset(string levelName)
    {
        yield return new WaitForSecondsRealtime(1.1f);
        LoadLevel(levelName);
    }
}
