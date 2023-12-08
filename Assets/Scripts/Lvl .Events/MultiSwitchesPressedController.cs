using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiSwitchesPressedController : MonoBehaviour
{
    public List<ISwitch> switchs = new List<ISwitch>();

    public MMF_Player onSwitchesValid;
    public MMF_Player onSwitchesInvalid;

    private void Awake()
    {
        foreach (var switchController in switchs)
        {
            UnityEvent callbackUnityEvent = new UnityEvent();
            callbackUnityEvent.AddListener(CheckIfSwitchesAreValid);
            MMF_Events callbackMMFEvent = new MMF_Events();
            callbackMMFEvent.Label = "Multi Switch Call Back";
            callbackMMFEvent.PlayEvents = callbackUnityEvent;
            switchController.onActivatedPlayer.AddFeedback(callbackMMFEvent);
        }
    }

    //Added to Switches to be called when they are activated
    private void CheckIfSwitchesAreValid()
    {
        bool allAreValid = true;
        foreach (var switchController in switchs)
        {
            if (!switchController.GetPressedState())
            {
                allAreValid = false;
                break;
            }
        }
        if (allAreValid)
        {
            onSwitchesValid?.PlayFeedbacks();
        }
        else
        {
            onSwitchesInvalid?.PlayFeedbacks();
        }
    }
}
