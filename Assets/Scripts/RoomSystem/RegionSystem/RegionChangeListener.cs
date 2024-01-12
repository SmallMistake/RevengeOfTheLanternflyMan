using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// This class can be attached to any object that needs to track when the player changes area
public class RegionChangeListener : MonoBehaviour,
    MMEventListener<RegionChangeEvent>
{
    [SerializeField]
    private UnityEvent<string> onRegionChange;

    protected void OnEnable()
    {
        this.MMEventStartListening<RegionChangeEvent>();
    }

    protected void OnDisable()
    {
        this.MMEventStopListening<RegionChangeEvent>();
    }

    public void OnMMEvent(RegionChangeEvent areaChangeInfo)
    {
        onRegionChange?.Invoke(areaChangeInfo.newRegionName);
    }
}
