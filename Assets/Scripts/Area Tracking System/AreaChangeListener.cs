using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// This class can be attached to any object that needs to track when the player changes area
public class AreaChangeListener : MonoBehaviour,
    MMEventListener<AreaChangeEvent>
{
    [SerializeField]
    private UnityEvent<string> onAreaChange;

    public void OnMMEvent(AreaChangeEvent areaChangeInfo)
    {
        onAreaChange?.Invoke(areaChangeInfo.newAreaName);
    }
}
