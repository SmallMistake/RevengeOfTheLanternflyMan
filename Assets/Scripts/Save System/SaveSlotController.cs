using IntronDigital;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveSlotController : MonoBehaviour, MMEventListener<TopDownEngineSaveFilesChangedEvent>
{
    public int playerID = 1;
    public List<GameObject> activeSaveSlotGameObjects;
    public List<GameObject> noSaveSlotGameObjects;
    public GameObject noSaveSlotContainer;
    // Start is called before the first frame update
    void OnEnable()
    {
        this.MMEventStartListening<TopDownEngineSaveFilesChangedEvent>();
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
        }
    }

    public void CreateSaveFile()
    {
        FindObjectOfType<ProgressManager>().CreateSaveGame(playerID);
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
