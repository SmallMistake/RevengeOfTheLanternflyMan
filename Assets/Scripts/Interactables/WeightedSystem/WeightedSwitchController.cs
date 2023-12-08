using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Switch that can be turned on when weighted objects are put on it
/// </summary>
public class WeightedSwitchController : ISwitch
{
    [Tooltip("Objects currently weighing down switch")]
    List<GameObject> objectsOnTrigger = new List<GameObject>();

    /// <summary>
    /// On trigger enter, add to weighted list if needed and then trigger events if needed
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weighted"))
        {
            int previousNumberOfThingsOnSwitch = objectsOnTrigger.Count;
            if (!(objectsOnTrigger.Contains(collision.gameObject)))
            {
                objectsOnTrigger.Add(collision.gameObject);
                if (previousNumberOfThingsOnSwitch == 0)
                {
                    TriggerSwitch(true);
                }
            }
        }
    }

    /// <summary>
    /// On trigger exit, remove from weighted list if needed and then trigger events if needed
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weighted"))
        {
            if (objectsOnTrigger.Contains(collision.gameObject))
            {
                objectsOnTrigger.Remove(collision.gameObject);
            }

            if (objectsOnTrigger.Count == 0)
            {

                TriggerSwitch(false);
            }
        }
    }
}
