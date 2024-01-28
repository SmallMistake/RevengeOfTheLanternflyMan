using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Added to the tilemap that visualizes a room on the minimap to help it visualize
/// </summary>
public class MinimapRoomController : RoomChangeListener,
    MMEventListener<MinimapEvent>
{
    protected override void Awake()
    {
        this.MMEventStartListening<MinimapEvent>();
        base.Awake();
    }

    protected override void OnDestroy()
    {
        this.MMEventStopListening<MinimapEvent>();
        base.OnDestroy();
    }

    public void OnMMEvent(MinimapEvent eventType)
    {
        switch(eventType.minimapState){
            case "Opened":
                RefreshVisual();
                break;

        }
    }

    /// <summary>
    /// Things like the rooms color is updated
    /// </summary>
    private void RefreshVisual()
    {
        if (roomToListenFor.IsRoomActive())
        {
            onRoomEnter?.Invoke();
        }
        else
        {
            onRoomExit?.Invoke();
        }
    }
}
