using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoomChangeEvent
{
    public string roomID;
    public bool entered;

    public RoomChangeEvent(string roomID, bool entered)
    {
        this.roomID = roomID;
        this.entered = entered;
    }

    static RoomChangeEvent e;
    public static void Trigger(string roomID, bool entered)
    {
        e.roomID = roomID;
        e.entered = entered;
        MMEventManager.TriggerEvent(e);
    }
}
