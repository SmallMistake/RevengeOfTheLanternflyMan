using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is added to objects that can be climbed to provide information about the climbable object.
/// </summary>
public class ClimbablePoint : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The offset that will be applied to the player when they exit the climbable object")]
    public Vector3 exitOffset;

    public Vector3 GetExitOffset()
    {
        return exitOffset;
    }
}
