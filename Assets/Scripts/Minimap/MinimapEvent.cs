using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MinimapEvent
{
    public string minimapState;

    public MinimapEvent(string minimapState)
    {
        this.minimapState = minimapState;
    }

    static MinimapEvent e;
    public static void Trigger(string minimapState)
    {
        e.minimapState = minimapState;
        MMEventManager.TriggerEvent(e);
    }
}
