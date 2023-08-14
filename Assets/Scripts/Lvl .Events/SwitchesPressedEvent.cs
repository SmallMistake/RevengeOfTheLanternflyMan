using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchesPressedEvent : MonoBehaviour
{
    public List<SwitchController> switchControllers = new List<SwitchController>();

    public UnityEvent onSwitchesValid;
    public UnityEvent onSwitchesInvalid;

    private void OnEnable()
    {
        foreach (var switchController in switchControllers)
        {
            switchController.onSwitchPressed += CheckIfSwitchesAreValid;
        }
    }

    public void CheckIfSwitchesAreValid(bool value)
    {
        bool allAreValid = true;
        foreach (var switchController in switchControllers)
        {
            if (!switchController.isOn())
            {
                allAreValid = false;
                break;
            }
        }
        if (allAreValid)
        {
            onSwitchesValid?.Invoke();
        }
        else
        {
            onSwitchesInvalid?.Invoke();
        }
    }
}
