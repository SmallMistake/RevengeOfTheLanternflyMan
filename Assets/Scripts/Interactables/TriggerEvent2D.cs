using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent2D : MonoBehaviour
{
    public TagMask acceptedTags;
    public UnityEvent<GameObject> onTriggerEnter;
    public UnityEvent<GameObject> onTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (acceptedTags.IsInTagMask(collision.tag))
        {
            onTriggerEnter.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (acceptedTags.IsInTagMask(collision.tag))
        {
            onTriggerExit.Invoke(collision.gameObject);
        }
    }
}
