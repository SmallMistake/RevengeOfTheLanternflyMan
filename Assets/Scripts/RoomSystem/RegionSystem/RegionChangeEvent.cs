using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An event triggered every time the player changes the current area they are in
/// </summary>
public struct RegionChangeEvent
{
    public string newRegionID;
    public string newRegionName;

    public RegionChangeEvent(string newRegionID, string newRegionName)
    {
        this.newRegionID = newRegionID;
        this.newRegionName = newRegionName;
    }

    static RegionChangeEvent e;
    public static void Trigger(string newRegionID, string newRegionName)
    {
        e.newRegionID = newRegionID;
        e.newRegionName = newRegionName;
        MMEventManager.TriggerEvent(e);
    }
}