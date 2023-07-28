using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainedItemSubObjective : ISubObjectiveTrigger
    { 
    public Utils.PermanentUpgrades upgradeNameToCheckFor;

    public Action<string, bool> getTriggerAction()
    {
        throw new NotImplementedException();
    }

    void Start()
    {
        InventoryController.upgradesChanged += WatchPlayerInventory;
    }

    private void OnDestroy()
    {
        InventoryController.upgradesChanged -= WatchPlayerInventory;
    }

    private void WatchPlayerInventory(Utils.PermanentUpgrades upgradeName, int changeDirection)
    {
        if (upgradeNameToCheckFor.Equals(upgradeName))
        {
            ISubObjectiveTrigger.SubObjectiveChanged.Invoke(subObjectiveName, true);
        }
    }
}
