using MoreMountains.Feedbacks;
using UnityEngine;

/// <summary>
/// Switch type interactables can be made by extending ISwitch
/// </summary>
public class ISwitch : MonoBehaviour
{
    [Tooltip("Is the switch pressed")]
    private bool pressed = false;
    [Tooltip("Called when the switch is pressed")]
    public MMF_Player onActivatedPlayer;
    [Tooltip("Called when the switch is un pressed")]
    public MMF_Player onDeactivatedPlayer;

    void Start()
    {
        HandleCurrentState();
    }

    /// <summary>
    /// Flip the current state of the switch
    /// </summary>
    internal void TriggerSwitch()
    {
        pressed = !pressed;
        HandleCurrentState();
    }

    /// <summary>
    /// Set the current state specifically
    /// </summary>
    /// <param name="state"></param>
    internal void TriggerSwitch(bool state)
    {
        pressed = state;
        HandleCurrentState();
    }

    /// <summary>
    /// Check to see what the switches current state is. Then, call the correct feedback
    /// </summary>
    private void HandleCurrentState()
    {
        if (pressed)
        {
            onActivatedPlayer.PlayFeedbacks();
        } else { 
            onDeactivatedPlayer.PlayFeedbacks();
        }
    }
    
    public bool GetPressedState()
    {
        return pressed;
    }
}
