using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

///Summary <summary>
/// This script is used to change the floor of the object it is attached to.
/// </summary>
public class FloorSwitcherController : MonoBehaviour
{
    [Tooltip("The current floor the object is on. 1 is the first floor, 2 is the second floor, ect")]
    public int currentFloor;

    [Tooltip("Tracks which areas the object is currently in that can change the floor")]
    public List<FloorTransitionArea> transitionAreasCurrentlyIn;

    /// <summary>
    /// Triggered when the object enters a transition area.
    /// Adds to areas list and then checks if the floor needs to be changed.
    /// </summary>
    /// <param name="transitionArea">Trigger area that can change the objects floor level</param>
    public void EnterTransitionArea(FloorTransitionArea transitionArea)
    {
        transitionAreasCurrentlyIn.Add(transitionArea);
        CheckIfChangeFloor();
    }

    /// <summary>
    /// Triggered when the object exits a transition area
    /// Removes from areas list and then checks if the floor needs to be changed.
    /// </summary>
    /// <param name="transitionArea">Trigger area left that can change the objects floor level</param>
    public void ExitTransitionArea(FloorTransitionArea transitionArea)
    {
        transitionAreasCurrentlyIn.Remove(transitionArea);
        CheckIfChangeFloor();
    }
    
    /// <summary>
    /// Called to check list to see if the floor needs to be changed and then changes it if needed.
    /// </summary>
    private void CheckIfChangeFloor()
    {
        if(transitionAreasCurrentlyIn.Count == 1)
        {
            MoveFloor(transitionAreasCurrentlyIn[0].floorToGoTo);
        }
    }

    /// <summary>
    /// Move floors
    /// </summary>
    /// <param name="newFloor">floor to move to</param>
    private void MoveFloor(int newFloor)
    {
        currentFloor = newFloor;
        SortingGroup sortingGroup = GetComponent<SortingGroup>();
        sortingGroup.sortingLayerName =  GetFloorVersionOfName(sortingGroup.sortingLayerName, currentFloor);

        gameObject.layer = LayerMask.NameToLayer(GetFloorVersionOfName(LayerMask.LayerToName(gameObject.layer), currentFloor));
    }

    /// <summary>
    /// Used to conver a layer name to it's floor version.
    /// </summary>
    /// <param name="originalName">original name to convert</param>
    /// /// <param name="floorToGoTo">floor to apply</param>
    private string GetFloorVersionOfName(string originalName, int floorToGoTo)
    {
        if (floorToGoTo == 1)
        {
            if (originalName.Contains("2F"))
            {
                return originalName.Substring(2);
            }
        }
        else if (floorToGoTo == 2)
        {
            if (!originalName.Contains("2F"))
            {
                return "2F" + originalName;
            }
        }
        return originalName;
    }
}
