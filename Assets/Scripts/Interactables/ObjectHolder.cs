using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [SerializeField]
    private Holdable objectBeingHeld;

    [SerializeField] 
    private GameObject objectHoldLocation;

    [SerializeField]
    private GameObject dropLocation;

    [SerializeField]
    private List<GameObject> objectsInRange; //Use by the water pour out to see if anything is in range of being filled.

    [SerializeField]
    private List<Holdable> holdablesInRange = new List<Holdable>();

    //return true if now holding an object
    public bool? HandleButtonPress()
    {
        if(objectBeingHeld == null)
        {
            if (TryToPickupObject())
            {
                return true;
            }
            else
            {
                return null;
            }
        }
        else
        {
            DropObject();
            return false;
        }
    }

    public bool TryToPickupObject()
    {
        if(holdablesInRange.Count > 0)
        {
            Holdable holdable = holdablesInRange[0];
            holdable.PickUp(objectHoldLocation);
            holdablesInRange.RemoveAt(0);
            objectsInRange.Remove(holdable.gameObject);
            objectBeingHeld = holdable;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DropObject()
    {
        objectBeingHeld.PutDown(dropLocation.transform.position);
        holdablesInRange.Add(objectBeingHeld);
        objectsInRange.Add(objectBeingHeld.gameObject);
        objectBeingHeld = null;
    }

    //Catalog objects in range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Holdable holdable = collision.gameObject.GetComponent<Holdable>();
        if(holdable != null)
        {
            if (!holdablesInRange.Contains(holdable))
            {
                holdablesInRange.Add(holdable);
            }
        }

        if (!objectsInRange.Contains(collision.gameObject))
        {
            objectsInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Holdable holdable = collision.gameObject.GetComponent<Holdable>();
        if (holdable != null)
        {
            if (holdablesInRange.Contains(holdable))
            {
                holdablesInRange.Remove(holdable);
            }
        }

        if (objectsInRange.Contains(collision.gameObject))
        {
            objectsInRange.Remove(collision.gameObject);
        }
    }

    public bool TryToUse()
    {
        if (objectBeingHeld)
        {
            objectBeingHeld.Use(objectsInRange);
            return true;
        }
        else
        {
            return false;
        }
    }
}
