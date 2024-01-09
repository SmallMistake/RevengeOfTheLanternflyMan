using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class tracks the current area the object is in. Useful for things like area notifications
public class AreaChangeEmitter : MonoBehaviour
{
    [Tooltip("Tracks what area the object currently is in.")]
    private string currentAreaID;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AreaController areaController = collision.gameObject.GetComponent<AreaController>();

        if (areaController != null)
        {
            if (areaController.areaID != currentAreaID)
            {
                currentAreaID = areaController.areaID;
                AreaChangeEvent.Trigger(areaController.areaID, areaController.areaName);
            }
        }
    }
}
