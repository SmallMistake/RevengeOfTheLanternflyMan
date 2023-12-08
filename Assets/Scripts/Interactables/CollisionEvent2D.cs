using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is a utility helper that can be added to any object with collision to call events on collision events
/// </summary>
public class CollisionEvent2D : MonoBehaviour
{
    [Tooltip("Tags that will result in invocations")]
    public TagMask acceptedTags;
    [Tooltip("Events to call on collision enter")]
    public UnityEvent<GameObject> onCollisionEnter;
    [Tooltip("Events to call on collision exit")]
    public UnityEvent<GameObject> onCollisionExit;

    /// <summary>
    /// On Collision Enter, check if the tag is allowed and then call registered events
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (acceptedTags.IsInTagMask(collision.gameObject.tag))
        {
            onCollisionEnter?.Invoke(collision.gameObject);
        }
    }

    /// <summary>
    /// On Collision Exit, check if the tag is allowed and then call registered events
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (acceptedTags.IsInTagMask(collision.gameObject.tag))
        {
            onCollisionExit?.Invoke(collision.gameObject);
        }
    }
}
