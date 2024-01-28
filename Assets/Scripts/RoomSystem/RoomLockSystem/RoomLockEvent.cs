using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Emitting this event when a room lock event occurs.
/// </summary>
public struct RoomLockEvent
{
    public string roomID;
    public bool locked;

    public RoomLockEvent(string roomID, bool locked)
    {
        this.roomID = roomID;
        this.locked = locked;
    }

    static RoomLockEvent e;
    public static void Trigger(string roomID, bool locked)
    {
        e.roomID = roomID;
        e.locked = locked;
        MMEventManager.TriggerEvent(e);
    }
}
