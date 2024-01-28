using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MinimapMarkerController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onMarkerPlaced;
    [SerializeField]
    private UnityEvent onMarkerRemoved;

    public void MarkerPlaced()
    {
        onMarkerPlaced?.Invoke();
    }

    public void MarkerRemoved()
    {
        onMarkerRemoved?.Invoke();
    }
}
