using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Holdable : MonoBehaviour
{
    [SerializeField]
    private bool beingHeld = false;

    [Tooltip("the area that displays the interact key")]
    public GameObject triggerArea;

    public UnityEvent<List<GameObject>> onUse;

    public void PickUp(GameObject placeToHold) {
        beingHeld = !beingHeld;
        gameObject.transform.SetParent(placeToHold.transform);
        gameObject.transform.localPosition = Vector3.zero;
        if (triggerArea)
        {
            triggerArea.SetActive(false);
        }
    }

    public void PutDown(Vector3 locationToDrop)
    {
        gameObject.transform.SetParent(null); //Make this able to find a parent in the future. Else this will bug.
        gameObject.transform.position = locationToDrop;
        if (triggerArea)
        {
            triggerArea.SetActive(true);
        }
    }
    

    public virtual void Use(List<GameObject> objectsInRange)
    {
        onUse?.Invoke(objectsInRange); 
    }
}
