using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystemGameObject : MonoBehaviour
{

    //Outdated
    private PlayerData data;
    private QuestLineData questData;

    private static SaveSystemGameObject saveInstance; //used to delete if one already exists
    public static event Action<PlayerData> loadedPlayer; //Used by Observers

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (saveInstance == null)
        {
            saveInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadPlayer();
    }
    private void Start()
    {
        loadedPlayer?.Invoke(data);
    }

    public void SavePlayer()
    {
        //Todo Replace
        //InventoryController playerInventory = FindObjectOfType<InventoryController>();
        SaveSystem.SavePlayer(
            currency: -1,//playerInventory.GetCurrency(),
            level: data.level,
            timePlayed: 4f,
            items: null// playerInventory.GetUpgrades()
        );
        //Debug.Log($"Saved Player: Upgrades = {playerInventory.GetUpgrades().Count} Currency = playerInventory.GetCurrency()");
    }

    public void UILoadPlayer()
    {
        LoadPlayer();
    }

    public PlayerData LoadPlayer()
    {
        data = SaveSystem.LoadPlayer();
        loadedPlayer?.Invoke(data);
        return data;
    }

    public void UIResetPlayer()
    {
        ResetPlayerData();
    }

    public PlayerData ResetPlayerData()
    {
        SaveSystem.SavePlayer(0, -99, 0, new List<Utils.PermanentUpgrades>());
        return LoadPlayer();
    }

    public void CreateNewSaveFile()
    {
        SaveSystem.SavePlayer(0, 0, 0, new List<Utils.PermanentUpgrades>());
    }

    public PlayerData GetData()
    {
        return data;
    }

    public void LevelStarted()
    {
        loadedPlayer?.Invoke(data);
    }
}
