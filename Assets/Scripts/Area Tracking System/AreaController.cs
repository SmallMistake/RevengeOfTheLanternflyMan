using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to triggers that reprsent transition points to a new area. 
/// When a player enters this controllers trigger they will use the data in this class to set area values.
/// </summary>
public class AreaController : MonoBehaviour
{
    [Tooltip("Unique ID of Area")]
    public string areaID;
    [Tooltip("Human Friendly Area Name")]
    public string areaName;
    //Can be extended to add on extra area info
}
