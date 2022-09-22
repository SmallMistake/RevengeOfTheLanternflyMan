using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int numberOfAcorns;
    private int numberOfKeys;

    private bool unlockedVenusFlyTrap;
    private bool unlockedWalnuts;
    private bool unlockedPesticide;

    public static event Action<int> acornsChanged;

    public static event Action<int> keysChanged;

    private void Start()
    {
        acornsChanged.Invoke(numberOfAcorns);
        keysChanged.Invoke(numberOfKeys);
    }

    public void AddAcorn(int amount)
    {
        numberOfAcorns += amount;
        acornsChanged.Invoke(numberOfAcorns);
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

    public bool UnlockedPesticide()
    {
        return unlockedPesticide;
    }

    public bool UnlockedWalnuts()
    {
        return unlockedWalnuts;
    }

    public bool UnlockedVenusFlyTrap()
    {
        return unlockedVenusFlyTrap;
    }

    public void UnlockUpgrade(Utils.PermanentUpgrades upgradeName)
    {
        switch (upgradeName) {
            case Utils.PermanentUpgrades.VenusFlyTrap:
                unlockedVenusFlyTrap = true;
                break;
            case Utils.PermanentUpgrades.Pecticide:
                unlockedPesticide = true;
                break;
            case Utils.PermanentUpgrades.Walnut:
                unlockedWalnuts = true;
                break;

        }
    }
}
