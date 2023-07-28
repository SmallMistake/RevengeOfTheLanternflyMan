using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int numberOfAcorns;
    private int maxAcorns = 9;
    private int numberOfKeys;

    private List<Utils.PermanentUpgrades> permanentUpgrades = new List<Utils.PermanentUpgrades>();

    public static event Action<int> acornsChanged;

    public static event Action<int> keysChanged;
    public static event Action<Utils.PermanentUpgrades, int> upgradesChanged; //Use -1 if lost upgrade, 1 if gained
    public static event Action<Utils.PermanentUpgrades?> changedPrimary;
    public static event Action<Utils.PermanentUpgrades?> changedSecondary;

    private void Awake()
    {
        SaveSystemGameObject.loadedPlayer += LoadedPlayer;
    }


    private void Start()
    {
        UpdateUIWithCurrentSetItems();
        FindObjectOfType<SaveSystemGameObject>().LoadPlayer();
        acornsChanged?.Invoke(numberOfAcorns);
        keysChanged?.Invoke(numberOfKeys);
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
        if (numberOfAcorns + amount < maxAcorns)
        {
            numberOfAcorns += amount;
        }
        else
        {
            numberOfAcorns = maxAcorns;
        }
        acornsChanged?.Invoke(numberOfAcorns);
    }

    public int GetCurrency()
    {
        return numberOfAcorns;
    }

    public bool UseWalnut()
    {
        if(numberOfAcorns > 0)
        {
            numberOfAcorns--;
            acornsChanged?.Invoke(numberOfAcorns);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddKey(int amount)
    {
        numberOfKeys += amount;
        keysChanged?.Invoke(numberOfKeys);
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
        upgradesChanged?.Invoke(upgradeName, 1);
        UpdateUIWithCurrentSetItems();
    }

    public List<Utils.PermanentUpgrades> GetUpgrades()
    {
        return permanentUpgrades;
    }

    private void UpdateUIWithCurrentSetItems()
    {
        if (permanentUpgrades.Contains(Utils.PermanentUpgrades.Pecticide)) // This is just a temp solution
        {
            changedPrimary?.Invoke(Utils.PermanentUpgrades.Pecticide);
        }
        if (permanentUpgrades.Contains(Utils.PermanentUpgrades.Walnut))
        {
            changedSecondary?.Invoke(Utils.PermanentUpgrades.Walnut);
        }
    }
}
