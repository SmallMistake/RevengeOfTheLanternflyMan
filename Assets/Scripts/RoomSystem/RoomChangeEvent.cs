using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoomChangeEvent
{
    public string roomID;

    public RoomChangeEvent(string roomID)
    {
        this.roomID = roomID;
    }

    static RoomChangeEvent e;
    public static void Trigger(string roomID)
    {
        e.roomID = roomID;
        MMEventManager.TriggerEvent(e);
    }
}
