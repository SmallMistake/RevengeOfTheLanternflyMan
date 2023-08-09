using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitchOnAreaEnter : MonoBehaviour
{
    public List<ISwitch> switchesToTrigger = new List<ISwitch>();
    public List<GameObject> objectsToTrigger = new List<GameObject>();
    private List<Triggerable> triggers = new List<Triggerable> ();
    private bool firstTime = true;

    private void Start()
    {
        foreach (GameObject triggerObject in objectsToTrigger)
        {
            triggers.Add(triggerObject.GetComponent<Triggerable>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && firstTime)
        {
            firstTime = false;
            foreach (ISwitch switchTrigger in switchesToTrigger)
            {
                switchTrigger.TriggerSwitch();
            }

            foreach (Triggerable triggerable in triggers)
            {
                triggerable.trigger();
            }
        }
    }
}
