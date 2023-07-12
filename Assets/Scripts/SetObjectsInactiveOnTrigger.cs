using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectsInactiveOnTrigger : MonoBehaviour, Triggerable
{
    public List<GameObject> objectsToMakeInactive = new List<GameObject>();

    public void trigger()
    {
        foreach (GameObject objectToActivate in objectsToMakeInactive)
        {
            objectToActivate.SetActive(false);
        }
    }
}
