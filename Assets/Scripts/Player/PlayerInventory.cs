using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int numberOfAcorns;
    private int numberOfKeys;

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
}
