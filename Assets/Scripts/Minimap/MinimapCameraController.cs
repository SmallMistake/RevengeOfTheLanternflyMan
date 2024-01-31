using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField]
    private InputActionReference zoomAxisInputReference;

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

    private void OnEnable()
    {
        zoomAxisInputReference.action.performed += HandleZoomControls;
    }

    private void OnDisable()
    {
        zoomAxisInputReference.action.performed -= HandleZoomControls;
    }

    private void HandleZoomControls(InputAction.CallbackContext actionContext)
    {
        //TODO make this increase and decrease as an axis
        if (actionContext.ReadValueAsButton())
        {
            print("TODO ZOOM IN");
        } else
        {
            print("TODO ZOOM OUT");
        }
    }
}
