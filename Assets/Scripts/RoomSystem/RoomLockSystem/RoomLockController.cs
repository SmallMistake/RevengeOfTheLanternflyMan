using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is added to room locks to designate the object as a room lock and pass on data to the emitter that enters it.
/// Must be connected to room listener or master lock controller
/// </summary>
public class RoomLockController : MonoBehaviour
{
    //This is auto set when this lock is added to a master lock controller
    internal UnityEvent intialLockCallbacks = new UnityEvent();

    public UnityEvent onLockFeedback;
    public UnityEvent onUnlockFeedback;
    public UnityEvent setupFeedback;

    [Tooltip("When the player exits the lock area the locks activate. Turn off with feedback to prevent relock")]
    [SerializeField]
    public bool autoLocksOnTriggerExit = true;

    private bool locked = false;

    internal bool IsLocked() { return locked; }

    public void SetAutoLocksOnTriggerExit(bool value)
    {
        autoLocksOnTriggerExit = value;
    }

    public void HandleLock()
    {
        if (!locked)
        {
            onLockFeedback?.Invoke();
            locked = true;
        }
    }

    public void HandleUnlock() 
    {
        locked = false;
        onUnlockFeedback?.Invoke();
    }

    public void SetupLock()
    {
        setupFeedback?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (!locked)
            {
                if (autoLocksOnTriggerExit)
                {
                    //HandleLock(); TO be handled by master lock controller. THis is to controll if the room is still active or not.
                    intialLockCallbacks.Invoke();
                }
            }
        }
    }
}
