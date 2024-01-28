using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class listens for room changes and calls subscribers
/// </summary>
public class RoomChangeListener : MonoBehaviour,
    MMEventListener<RoomChangeEvent>
{
    [Tooltip("Fire off Event when room entered has a matching ID")]
    [SerializeField]
    protected RoomController roomToListenFor;

    [SerializeField]
    protected UnityEvent onRoomEnter;

    [SerializeField]
    protected UnityEvent onRoomExit;

    protected virtual void Awake()
    {
        this.MMEventStartListening<RoomChangeEvent>();
    }

    protected virtual void OnDestroy()
    {
        this.MMEventStopListening<RoomChangeEvent>();
    }

    public void OnMMEvent(RoomChangeEvent regionLockInfo)
    {
        if(regionLockInfo.roomID == roomToListenFor.name)
        {
            if(regionLockInfo.entered)
            {
                //print($"TODO Room Changed {regionLockInfo.roomID}");
                onRoomEnter.Invoke();
            }
            else
            {
                //print($"TODO Room Left {regionLockInfo.roomID}");
                onRoomExit.Invoke();
            }
        }
    }
}
