using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent2D : MonoBehaviour
{
    public TagMask acceptedTags;
    public UnityEvent<GameObject> onCollisionEnter;
    public UnityEvent<GameObject> onCollisionExit;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (acceptedTags.IsInTagMask(collision.gameObject.tag))
        {
            onCollisionEnter?.Invoke(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (acceptedTags.IsInTagMask(collision.gameObject.tag))
        {
            onCollisionExit?.Invoke(collision.gameObject);
        }
    }
}
