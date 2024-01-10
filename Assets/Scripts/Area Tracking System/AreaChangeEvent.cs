using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An event triggered every time the player changes the current area they are in
/// </summary>
public struct AreaChangeEvent
{
    public string newAreaID;
    public string newAreaName;

    public AreaChangeEvent(string newAreaID, string newAreaName)
    {
        this.newAreaID = newAreaID;
        this.newAreaName = newAreaName;
    }

    static AreaChangeEvent e;
    public static void Trigger(string newAreaID, string newAreaName)
    {
        e.newAreaID = newAreaID;
        e.newAreaName = newAreaName;
        MMEventManager.TriggerEvent(e);
    }
}