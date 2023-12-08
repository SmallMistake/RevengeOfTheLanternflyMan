using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to a trigger area to give fillables to any fillables in the area
/// </summary>
public class ContainerFiller : MonoBehaviour
{
    [Tooltip("Name of the fillable being distributed")]
    public string fillName;
}
