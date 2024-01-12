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
    [SerializeField]
    private UnityEvent onRoomEnter;

    protected void Awake()
    {
        this.MMEventStartListening<RoomChangeEvent>();
    }

    protected void OnDestroy()
    {
        this.MMEventStopListening<RoomChangeEvent>();
    }

    public void OnMMEvent(RoomChangeEvent regionLockInfo)
    {
            print("TODO Room Changed");
            //RegionChangeEvent.Invoke();
    }
}
