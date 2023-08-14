using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocksUsedEvent : MonoBehaviour
{
    public List<KeyOperatedZone> keyControllers = new List<KeyOperatedZone>();

    public UnityEvent onkeysValid;
    public UnityEvent onkeysInvalid;

    private void OnEnable()
    {
        foreach (var keyController in keyControllers)
        {
            keyController.OnActivation.AddListener(CheckIfSwitchesAreValid);
        }
    }

    private void OnDisable()
    {
        foreach (var keyController in keyControllers)
        {
            keyController.OnActivation.RemoveListener(CheckIfSwitchesAreValid);
        }
    }

    public void CheckIfSwitchesAreValid()
    {
        bool allAreValid = true;
        foreach (var keyController in keyControllers)
        {
            if (keyController.Activable)
            {
                allAreValid = false;
                break;
            }
        }
        if (allAreValid)
        {
            onkeysValid?.Invoke();
        }
        else
        {
            onkeysInvalid?.Invoke();
        }
    }
}
