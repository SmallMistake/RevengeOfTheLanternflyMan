using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MinimapCursorController : MonoBehaviour
{

    [SerializeField]
    private GameObject markerPrefab;
    [SerializeField]
    private List<MinimapMarkerController> markersInCursor = new List<MinimapMarkerController>();

    private GameObject playerGameObject;

    [SerializeField]
    private InputActionReference placeMarkerInputReference;

    [SerializeField]
    private InputActionReference primaryMovementInputReference;

    private Rigidbody2D rb;
    [SerializeField]
    private float speed;

    //Animation Parameters
    protected const string _hoveringAnimationParameterName = "Hovering";
    protected int _hoveringAnimationParameter;

    protected void Awake()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    protected void OnEnable()
    {
        placeMarkerInputReference.action.performed += UseMarker;
        primaryMovementInputReference.action.performed += OnMovement;
        primaryMovementInputReference.action.Enable();
    }

    protected void OnDisable()
    {
        placeMarkerInputReference.action.performed -= UseMarker;
        primaryMovementInputReference.action.performed -= OnMovement;
        primaryMovementInputReference.action.Disable();
    }

    private void OnMovement(InputAction.CallbackContext actionContext)
    {
        Vector2 _moveInput = actionContext.action.ReadValue<Vector2>();
        rb.transform.position += new Vector3(_moveInput.x, _moveInput.y, 0) * speed;
    }

    private void ResetCursorPositionToPlayerPosition()
    {
        transform.position = playerGameObject.transform.position;
    }

    /// <summary>
    /// When the player uses a marker place it on the map.
    /// </summary>
    private void UseMarker(InputAction.CallbackContext actionContext)
    {
        if(markersInCursor.Count > 0)
        {
            MinimapMarkerEvent.Trigger(markersInCursor[0], MarkerStatus.Remove);
        }
        else
        {
            GameObject markerInstance = GameObject.Instantiate(markerPrefab);
            markerInstance.transform.position = transform.position;
            MinimapMarkerController markerController = markerInstance.GetComponent<MinimapMarkerController>();
            MinimapMarkerEvent.Trigger(markerController, MarkerStatus.Register);
        }
    }

    /// <summary>
    /// Add Markers that the cursor is over to a list to be used to tell if a marker is to be deleted or placed
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MinimapMarkerController markerController = collision.GetComponent<MinimapMarkerController>();
        if(markerController != null && !markersInCursor.Contains(markerController)) {
            markersInCursor.Add(markerController);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MinimapMarkerController markerController = collision.GetComponent<MinimapMarkerController>();
        if (markerController != null && markersInCursor.Contains(markerController))
        {
            markersInCursor.Remove(markerController);
        }
    }
}
