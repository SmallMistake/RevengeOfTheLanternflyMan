using AeLa.EasyFeedback.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<InventoryEntry> inventoryEntries;
    private List<Utils.PermanentUpgrades> permanentUpgrades = new List<Utils.PermanentUpgrades>();

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
    }

    private void OnDestroy()
    {
        SaveSystemGameObject.loadedPlayer -= LoadedPlayer;
    }

    private void LoadedPlayer(PlayerData playerData)
    {
        permanentUpgrades = playerData.items;
    }

    public void PickupItem(InventoryEntry newInventoryEntry)
    {
        bool itemNotInInventory = true;
        foreach (var listEntry in inventoryEntries.Where(listEntry => listEntry.Name == newInventoryEntry.Name))
        {
            listEntry.amountHeld += newInventoryEntry.amountHeld;
            itemNotInInventory = false;
        }
        if (itemNotInInventory)
        {
            inventoryEntries.Add(newInventoryEntry.ShallowCopy());
        }
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

    public bool TryToUseItem(String itemName, int amountNeeded)
    {
        foreach (var listEntry in inventoryEntries.Where(listEntry => listEntry.Name == itemName))
        {
            if(listEntry.amountHeld >= amountNeeded)
            {
                listEntry.amountHeld -= amountNeeded;
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
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
