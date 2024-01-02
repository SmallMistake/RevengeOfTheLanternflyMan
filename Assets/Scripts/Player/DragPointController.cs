using MoreMountains.TopDownEngine;
using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPointController : MonoBehaviour
{

    [Tooltip("Objects that can be grabbed onto")]
    [SerializeField]
    public List<Draggable> draggablesInRange = new List<Draggable>();

    [Tooltip("Used to Tell Character Orientation")]
    [SerializeField]
    CharacterOrientation2D characterOrientationController;

    [Tooltip("Current Orientation")]
    [SerializeField]
    Character.FacingDirections currentOrientation;

    [SerializeField]
    public Vector2 northOffset, southOffset, westOffset, eastOffset;

    [SerializeField]
    private Draggable currentObjectBeingDragged;

    private void OnEnable()
    {
        if(characterOrientationController == null)
        {
            characterOrientationController = GetComponent<CharacterOrientation2D>();
        }
        HandleOrientationChange(characterOrientationController.CurrentFacingDirection);
    }

    private void Update()
    {
        if(currentOrientation != characterOrientationController.CurrentFacingDirection)
        {
            HandleOrientationChange(characterOrientationController.CurrentFacingDirection);
        }
    }

    private void HandleOrientationChange(Character.FacingDirections orientation)
    {
        currentOrientation = characterOrientationController.CurrentFacingDirection;
        Vector2 offsetLocation;
        switch (currentOrientation)
        {
            case Character.FacingDirections.North:
                offsetLocation = northOffset;
                break;
            case Character.FacingDirections.South:
                offsetLocation = southOffset;
                break;
            case Character.FacingDirections.East:
                offsetLocation = eastOffset;
                break;
            case Character.FacingDirections.West:
                offsetLocation = westOffset;
                break;
            default:
                offsetLocation = Vector2.zero;
                break;
        }
        transform.localPosition = offsetLocation;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        // && !collision.isTrigger
        Draggable draggable = collision.gameObject.GetComponent<Draggable>();
        if(draggable != null && draggable != currentObjectBeingDragged)
        {
            draggablesInRange.Add(draggable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Draggable draggable = collision.gameObject.GetComponent<Draggable>();
        if (draggablesInRange.Contains(draggable))
        {
            draggablesInRange.Remove(draggable);
        }
    }

    public bool CanDrag()
    {
        if (draggablesInRange.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartDragging()
    {
        currentObjectBeingDragged = draggablesInRange[0];
        draggablesInRange.RemoveAt(0);
        currentObjectBeingDragged.StartDragging(transform);
    }

    public void StopDragging()
    {
        draggablesInRange.Add(currentObjectBeingDragged);
        currentObjectBeingDragged.StopDragging();
    }
}
