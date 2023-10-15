using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbablePoint : MonoBehaviour
{
    [SerializeField]
    public Vector3 exitOffset;

    public Vector3 GetExitOffset()
    {
        return exitOffset;
    }
}
