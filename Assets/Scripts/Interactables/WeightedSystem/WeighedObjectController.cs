using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script is added to objects that you want to be able to weigh down weighted switches.
/// Only contains data
/// </summary>
public class WeighedObjectController : MonoBehaviour
{
    [Tooltip("Weight of the object")]
    public float weight;
}
