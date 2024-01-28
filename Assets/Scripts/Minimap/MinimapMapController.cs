using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Added to the visual part of the minimap to aid in visualizing the map.
/// </summary>
public class MinimapMapController : MonoBehaviour
{
    private void OnEnable()
    {
        MinimapEvent.Trigger("Opened");
    }
}
