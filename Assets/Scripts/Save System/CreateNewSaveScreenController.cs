using IntronDigital;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CreateNewSaveScreenController : MonoBehaviour
{
    public int playerID;
    public string firstScenePath;

    public void SetupNewSaveScreen(int playerID)
    {
        this.playerID = playerID;
        gameObject.SetActive(true);
    }

    public void CreateNewSaveFile(string newName)
    {
        FindObjectOfType<ProgressManager>().CreateSaveGame(playerID, newName);
        gameObject.SetActive(false);
        SceneManager.LoadScene(firstScenePath);
    }
}
