using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FloorSwitcherController : MonoBehaviour
{
    public int currentFloor;
    public List<FloorTransitionArea> transitionAreasCurrentlyIn;

    public void EnterTransitionArea(FloorTransitionArea transitionArea)
    {
        transitionAreasCurrentlyIn.Add(transitionArea);
        CheckIfChangeFloor();
    }

    public void ExitTransitionArea(FloorTransitionArea transitionArea)
    {
        transitionAreasCurrentlyIn.Remove(transitionArea);
        CheckIfChangeFloor();
    }

    private void CheckIfChangeFloor()
    {
        if(transitionAreasCurrentlyIn.Count == 1)
        {
            MoveFloor(transitionAreasCurrentlyIn[0].floorToGoTo);
        }
    }

    private void MoveFloor(int newFloor)
    {
        currentFloor = newFloor;
        SortingGroup sortingGroup = GetComponent<SortingGroup>();
        sortingGroup.sortingLayerName =  GetFloorVersionOfName(sortingGroup.sortingLayerName, currentFloor);

        gameObject.layer = LayerMask.NameToLayer(GetFloorVersionOfName(LayerMask.LayerToName(gameObject.layer), currentFloor));
        /*
        var children = GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            child.gameObject.layer = LayerMask.NameToLayer(GetFloorVersionOfName(child.gameObject.layer.ToString(), currentFloor));
        }
        */
    }

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
