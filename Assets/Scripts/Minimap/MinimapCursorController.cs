using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MinimapCursorController : CharacterAbility
{

    [SerializeField]
    private GameObject markerPrefab;
    [SerializeField]
    private List<MinimapMarkerController> markersInCursor = new List<MinimapMarkerController>();

    private GameObject playerGameObject;

    [SerializeField]
    private InputActionReference placeMarkerInputReference;

    //Animation Parameters
    protected const string _hoveringAnimationParameterName = "Hovering";
    protected int _hoveringAnimationParameter;

    protected override void Awake()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        base.Awake();
    }

    protected override void OnEnable()
    {
        placeMarkerInputReference.action.performed += UseMarker;
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        placeMarkerInputReference.action.performed -= UseMarker;
        base.OnEnable();
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

    /// <summary>
    /// Adds required animator parameters to the animator parameters list if they exist
    /// </summary>
    protected override void InitializeAnimatorParameters()
    {
        RegisterAnimatorParameter(_hoveringAnimationParameterName, AnimatorControllerParameterType.Bool, out _hoveringAnimationParameter);
    }

    /// <summary>
    /// Sends the current speed and the current value of the Walking state to the animator
    /// </summary>
    public override void UpdateAnimator()
    {
        MMAnimatorExtensions.UpdateAnimatorBool(_animator, _hoveringAnimationParameter, (markersInCursor.Count > 0), _character._animatorParameters, _character.RunAnimatorSanityChecks);
    }
}
