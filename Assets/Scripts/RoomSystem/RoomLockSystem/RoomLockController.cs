using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is added to room locks to designate the object as a room lock and pass on data to the emitter that enters it.
/// </summary>
public class RoomLockController : MonoBehaviour
{
    [Tooltip("This is the room/event id that will be used to check if the player has entered a new room and listeners to this id will be activated if so")]
    [SerializeField]
    protected string regionID;
}
