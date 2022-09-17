using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int numberOfAcorns;

    public static event Action<int> acornsChanged;

    private void Start()
    {
        acornsChanged.Invoke(numberOfAcorns);
    }

    public void AddAcorn(int amount)
    {
        numberOfAcorns += amount;
        acornsChanged.Invoke(numberOfAcorns);
    }
}
