using IntronDigital;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotController : MonoBehaviour, MMEventListener<TopDownEngineSaveFilesChangedEvent>
{
    public int playerID = 1;
    public TextMeshProUGUI playerNameTextMesh;
    public TextMeshProUGUI currentStageTextMesh;
    public TextMeshProUGUI currentProgressTextMesh;
    public List<GameObject> activeSaveSlotGameObjects;
    public List<GameObject> noSaveSlotGameObjects;
    public UnityEvent<int> onReadyToCreateNewSave;
    public string levelSelectScreen = "Scn_LevelSelect_Menu";

    // Start is called before the first frame update
    void OnEnable()
    {
        this.MMEventStartListening<TopDownEngineSaveFilesChangedEvent>();
        SetupVisuals();
    }

    protected virtual void OnDisable()
    {
        this.MMEventStopListening<TopDownEngineSaveFilesChangedEvent>();
    }

    public void OnMMEvent(TopDownEngineSaveFilesChangedEvent eventType)
    {
        SetupVisuals();
    }

    private void SetupVisuals()
    {
        GameProgress? progress = GetSaveFile(playerID);
        if (progress == null)
        {
            foreach (var gameObject in activeSaveSlotGameObjects)
            {
                gameObject.SetActive(false);
            }
            foreach (var gameObject in noSaveSlotGameObjects)
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var gameObject in activeSaveSlotGameObjects)
            {
                gameObject.SetActive(true);
            }
            foreach (var gameObject in noSaveSlotGameObjects)
            {
                gameObject.SetActive(false);
            }
            playerNameTextMesh.text = progress.playerName;
        }
    }

    public void LoadGame()
    {
        FindObjectOfType<ProgressManager>().LoadSavedProgress(playerID);
        //SceneManager.LoadScene(levelSelectScreen);
    }

    public void OpenCreateNewSaveScreen()
    {
        onReadyToCreateNewSave?.Invoke(playerID);
    }

    GameProgress? GetSaveFile(int playerID)
    {
        return FindObjectOfType<ProgressManager>().GetSaveFile(playerID);
    }

    public void DeleteSaveFile()
    {
        FindObjectOfType<ProgressManager>().ResetProgress(playerID);
    }

    //Used for when the file is deleted and the new game button needs to be selected
    public void SetButtonSelected(GameObject buttonToSet)
    {
        EventSystem.current.SetSelectedGameObject(buttonToSet);
    }
}
