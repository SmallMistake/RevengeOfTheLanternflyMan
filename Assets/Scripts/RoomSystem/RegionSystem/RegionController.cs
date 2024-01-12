using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to triggers that reprsent transition points to a new area. 
/// When a player enters this controllers trigger they will use the data in this class to set area values.
/// </summary>
public class RegionController : MonoBehaviour,
    MMEventListener<RoomChangeEvent>
{
    [Tooltip("Unique ID of Area")]
    public string activateID;
    [Tooltip("Human Friendly Area Name")]
    public string regionName;
    //Can be extended to add on extra area info

    protected void OnEnable()
    {
        this.MMEventStartListening<RoomChangeEvent>();
    }

    protected void OnDisable()
    {
        this.MMEventStopListening<RoomChangeEvent>();
    }

    public void OnMMEvent(RoomChangeEvent regionLockInfo)
    {
        if (regionLockInfo.roomID == activateID)
        {
            //RegionChangeEvent.Invoke();
        }
    }
}
