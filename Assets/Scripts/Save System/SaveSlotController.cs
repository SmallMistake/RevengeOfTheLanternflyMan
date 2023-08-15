using IntronDigital;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotController : MonoBehaviour
{
    public int playerID = 1;
    public GameObject activeSaveSlotContainer;
    public GameObject noSaveSlotContainer;
    // Start is called before the first frame update
    void Start()
    {
        GameProgress? progress = GetSaveDetails(playerID);
        if (progress == null)
        {
            activeSaveSlotContainer.SetActive(false);
            noSaveSlotContainer.SetActive(true);
        }
        else
        {
            activeSaveSlotContainer.SetActive(true);
            noSaveSlotContainer.SetActive(false);
        }
    }

    GameProgress? GetSaveDetails(int playerID)
    {
        print("TODO Get Real Saves");
        return null;
        //return new GameProgress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
