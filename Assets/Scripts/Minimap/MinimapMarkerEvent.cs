using Microsoft.Win32;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MarkerStatus { Register, Remove}
/// <summary>
/// This event represents when the status of a minimap marker changes
/// </summary>
public struct MinimapMarkerEvent
{
    public MinimapMarkerController marker;
    public MarkerStatus status;

    public MinimapMarkerEvent(MinimapMarkerController marker, MarkerStatus status)
    {
        this.marker = marker;
        this.status = status;
    }

    static MinimapMarkerEvent e;
    public static void Trigger(MinimapMarkerController marker, MarkerStatus status)
    {
        e.marker = marker;
        e.status = status;
        MMEventManager.TriggerEvent(e);
    }
}
