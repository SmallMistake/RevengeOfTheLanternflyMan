using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectsActiveOnTrigger : MonoBehaviour, Triggerable
{
    public List<GameObject> objectsToMakeActive = new List<GameObject>();

    public void trigger()
    {
        foreach (GameObject objectToActivate in objectsToMakeActive)
        {
            objectToActivate.SetActive(true);
        }
    }
}
