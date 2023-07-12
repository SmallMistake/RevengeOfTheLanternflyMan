using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitch : ISwitch
{

    List<GameObject> objectsOnTrigger = new List<GameObject>();

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
                    SetSprite();
                }
            }
        }
    }

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
                SetSprite();
            }
        }
    }
}
