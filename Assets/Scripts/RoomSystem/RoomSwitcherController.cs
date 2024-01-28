using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSwitcherController : MonoBehaviour
{
    public List<RoomController> roomsIn = new List<RoomController>();
    private RoomController activeRoom;
    //public GameObject startRoom;
    TempHolderManager tempHolder;

    //"RoomBounds" used to be a used tag for this. It can now be removed as the script is used now.

    private void Awake()
    {
        tempHolder = FindObjectOfType<TempHolderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomController roomController = collision.GetComponent<RoomController>();
        if(roomController)
        {
            if (!roomsIn.Contains(roomController))
            {
                roomsIn.Add(roomController);
            }

            if (NeedToSwitchRoom())
            {
                SwitchRoom(roomsIn[0]);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RoomController roomController = collision.GetComponent<RoomController>();
        if (roomController)
        {
            if (roomsIn.Contains(roomController))
            {
                roomsIn.Remove(roomController);
            }

            if (NeedToSwitchRoom())
            {
                SwitchRoom(roomsIn[0]);
            }
        }
    }

    private bool NeedToSwitchRoom()
    {
        if(roomsIn.Count == 1 && roomsIn[0] != activeRoom)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SwitchRoom(RoomController roomToChangeTo)
    {
        tempHolder?.ClearTempHolder();
        activeRoom?.ExitRoom();
        RoomChangeEvent.Trigger(activeRoom?.name, false);
        activeRoom = roomToChangeTo;
        RoomChangeEvent.Trigger(activeRoom?.name, true);
        activeRoom?.EnterRoom();
    }
}
