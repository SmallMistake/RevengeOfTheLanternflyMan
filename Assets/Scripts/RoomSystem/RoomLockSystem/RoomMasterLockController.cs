using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to cordinate all sub locks that might be in a room easier
/// </summary>
public class RoomMasterLockController : MonoBehaviour
{
    /// <summary>
    /// Used to tell is the room this lock listens for is currently active. If not don't lock
    /// </summary>
    private bool roomIsActive = false;

    [Tooltip("Changed to track the current status of the room.")]
    private bool roomLocked = false;

    [Tooltip("Locks Unlocked At Start")]
    [SerializeField]
    private bool unlockedAtStart = true;

    [Tooltip("Add room locks here to easily coordinate them")]
    [SerializeField]
    private List<RoomLockController> locksInRoom;
    /*
    [Tooltip("Conditions that cause the room to lock")] 
    //TODO fill this our with a class that can be extended for things like enemiesActive ect for extra room lock conditions if needed in the future
    public GameObject roomLockConditions;
    */

    private void Awake()
    {
        if (unlockedAtStart)
        {
            PrepareLocks();
        }
    }

    /// <summary>
    /// To be hooked up to Room Change Listener
    /// </summary>
    public void HandleRoomEnter()
    {
        roomIsActive = true;
    }

    /// <summary>
    /// To be hooked up to Room Change Listener
    /// </summary>
    public void HandleRoomExit()
    {
        roomIsActive = false;
    }

    /// <summary>
    /// If the player leaves the room don't lock doors.
    /// </summary>
    /// <returns></returns>
    private bool CanRoomLock()
    {
        if(roomIsActive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Called at start to setup child locks
    /// </summary>
    private void PrepareLocks()
    {
        foreach (RoomLockController lockController in locksInRoom)
        {
            lockController.SetupLock();
            lockController.intialLockCallbacks.AddListener(HandleRoomLockAttempt);
        }
    }

    /// <summary>
    /// This should be called back when a lock locks;
    /// </summary>
    internal void HandleRoomLockAttempt()
    {
        if(CanRoomLock())
        {
            LockLocks();
        }
    }

    /// <summary>
    /// Called to Unlock Room
    /// </summary>
    public void HandleRoomUnlockAttempt()
    {
        if (roomLocked)
        {
            roomLocked = false;
            foreach (RoomLockController lockController in locksInRoom)
            {
                lockController.HandleUnlock();
            }
        }
    }

    private void LockLocks() {
        roomLocked = true;
        foreach (RoomLockController lockController in locksInRoom)
        {
            lockController.HandleLock();
        }
    }
}
