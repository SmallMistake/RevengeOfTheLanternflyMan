using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class listens to region lock events then calls subscribers
/// </summary>
public class RegionLockListener : MonoBehaviour,
    MMEventListener<RoomLockEvent>
{
    [Tooltip("The ID of the Region that is being listened to")]
    protected string roomID;

    public UnityEvent onLock;
    public UnityEvent onUnlock;

    protected void OnEnable()
    {
        this.MMEventStartListening<RoomLockEvent>();
    }

    protected void OnDisable()
    {
        this.MMEventStopListening<RoomLockEvent>();
    }

    public void OnMMEvent(RoomLockEvent regionLockInfo)
    {
        if(regionLockInfo.roomID == roomID)
        {
            if(regionLockInfo.locked)
            {
                onLock.Invoke();
            }
            else
            {
                onUnlock.Invoke();
            }
        }
    }
}
