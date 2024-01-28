using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the main controller that controls the Minimap Virtual Camera
/// </summary>
public class MinimapCameraController : MonoBehaviour
{
    //TODO Add Zooming 
    [SerializeField]
    private float minZoom, maxZoom;

    [SerializeField]
    private CinemachineVirtualCameraBase virtualCameraToControl;

    private MinimapCursorController minimapCursor;

    private void Awake()
    {
        minimapCursor = GameObject.FindObjectOfType<MinimapCursorController>();
        if (minimapCursor != null)
        {
            virtualCameraToControl.Follow = minimapCursor.transform;
        }
        else
        {
            print("Please Add Tilemap Cursor to Minimap Camera Controller");
        }
    }

    //TODO Add Zooming Listening

    private void FixedUpdate()
    {
        HandleZoomControls();
    }

    private void HandleZoomControls()
    {
        if (Input.GetButton("Player1_Shoot"))
        {
            print("TODO ZOOM IN");
        } else if (Input.GetButton("Player1_SecondaryShoot"))
        {
            print("TODO ZOOM OUT");
        }
    }
}
