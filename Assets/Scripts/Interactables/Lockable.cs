using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lockable : MonoBehaviour
{
    public bool unlocked;
    public string itemNameToUse;
    public int amountNeeded;
    public UnityEvent onUnlock;

    public void TryToUnlock()
    {
        if (!unlocked)
        {
            bool succeededInUnlocking = FindObjectOfType<InventoryController>().TryToUseItem(itemNameToUse, amountNeeded);
            if (succeededInUnlocking)
            {
                unlocked = !unlocked;
                onUnlock?.Invoke();
            }
        }
    }
}
