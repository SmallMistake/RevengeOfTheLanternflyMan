using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is what should save and load markers to the minimap, also listens for when markers are placed and registers them.
/// </summary>
public class MinimapMasterMarkerController : MonoBehaviour,
    MMEventListener<MinimapMarkerEvent>
{
    [SerializeField]
    private List<MinimapMarkerController> registeredMarkers;

    [SerializeField]
    private int maxMarkers = 10;

    private void OnEnable()
    {
        this.MMEventStartListening<MinimapMarkerEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<MinimapMarkerEvent>();
    }

    public void OnMMEvent(MinimapMarkerEvent markerEvent)
    {
        switch(markerEvent.status)
        {
            case MarkerStatus.Register:
                if(registeredMarkers.Count > maxMarkers)
                {
                    Destroy(markerEvent.marker.gameObject);
                        break;
                }
                registeredMarkers.Add(markerEvent.marker);
                markerEvent.marker.transform.SetParent(transform);
                markerEvent.marker.MarkerPlaced();
                break;
            case MarkerStatus.Remove:
                registeredMarkers.Remove(markerEvent.marker);
                markerEvent.marker.MarkerRemoved();
                break;
        }
    }
}
