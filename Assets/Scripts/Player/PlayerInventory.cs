using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int numberOfAcorns;
    private int numberOfKeys;

    private List<Utils.PermanentUpgrades> permanentUpgrades = new List<Utils.PermanentUpgrades>();

    public static event Action<int> acornsChanged;

    public static event Action<int> keysChanged;
    public static event Action<Utils.PermanentUpgrades, int> upgradesChanged; //Use -1 if lost upgrade, 1 if gained


    private void Start()
    {
        SaveSystemGameObject.loadedPlayer += LoadedPlayer;
        acornsChanged.Invoke(numberOfAcorns);
        keysChanged.Invoke(numberOfKeys);
    }

    private void OnDestroy()
    {
        SaveSystemGameObject.loadedPlayer -= LoadedPlayer;
    }

    private void LoadedPlayer(PlayerData playerData)
    {
        numberOfAcorns = 0;
        AddAcorn(playerData.currency);
        permanentUpgrades = playerData.items;
    }

    public void AddAcorn(int amount)
    {
        numberOfAcorns += amount;
        acornsChanged.Invoke(numberOfAcorns);
    }

    public int GetCurrency()
    {
        return numberOfAcorns;
    }

    public void AddKey(int amount)
    {
        numberOfKeys += amount;
        keysChanged.Invoke(numberOfKeys);
    }

    public int GetKeys()
    {
        return numberOfKeys;
    }

    public bool UnlockedItem(Utils.PermanentUpgrades upgradeToCheckFor)
    {
        if (permanentUpgrades.Contains(upgradeToCheckFor))
        {
            return true;
        }
        else
        {
            return false;
        } 
    }

    public void UnlockUpgrade(Utils.PermanentUpgrades upgradeName)
    {
        permanentUpgrades.Add(upgradeName);
        upgradesChanged.Invoke(upgradeName, 1);
    }

    public List<Utils.PermanentUpgrades> GetUpgrades()
    {
        return permanentUpgrades;
    }
}
