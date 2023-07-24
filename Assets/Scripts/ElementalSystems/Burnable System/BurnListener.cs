using Language.Lua;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BurnListener : MonoBehaviour
{
    public Burnable burnableToListenTo;

    [SerializeField] UnityEvent onBurn;
    [SerializeField] UnityEvent stopBurn;

    private void Awake()
    {
        burnableToListenTo.burningStateChanged += BurningStateChanged;
    }

    private void BurningStateChanged(bool newBurningState)
    {
        if (newBurningState)
        {
            onBurn?.Invoke();
        }
        else
        {
           stopBurn?.Invoke();
        }
    }

}
