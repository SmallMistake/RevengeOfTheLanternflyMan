using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSwitcherController : MonoBehaviour
{
    public List<GameObject> roomsIn = new List<GameObject>();
    public GameObject roomHolder;
    private List<GameObject> rooms = new List<GameObject>();
    private GameObject activeRoom;
    public GameObject startRoom;

    TempHolderManager tempHolder;

    private void Awake()
    {
        tempHolder = FindObjectOfType<TempHolderManager>();
        foreach (Transform room in roomHolder.transform)
        {
            rooms.Add(room.gameObject);
            if(room.gameObject != startRoom)
            {
                foreach (Transform child in room)
                {
                    child.gameObject.SetActive(false);
                }
            }
            startRoom.SetActive(true);
        }
    }

    private void Start()
    {
        startRoom.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("RoomBounds")){
            if (activeRoom == null)
            {
                roomsIn.Add(collision.gameObject);
                SetRoomActive(int.Parse(roomsIn[0].name.ToString()) - 1);
            }
            if (!roomsIn.Contains(collision.gameObject))
            {
                roomsIn.Add(collision.gameObject);
            }
            if (roomsIn.Count == 1)
            {
                SetRoomActive(int.Parse(roomsIn[0].name.ToString()) - 1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomBounds"))
        {
            roomsIn.Remove(collision.gameObject);
            if (roomsIn.Count == 1)
            {
                SetRoomActive(int.Parse(roomsIn[0].name.ToString()) - 1);
            }
        }
    }

    public void SetRoomActive(int roomToChangeTo)
    {
        tempHolder.ClearTempHolder();
        if (activeRoom != null)
        {
            foreach (Transform child in activeRoom.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        activeRoom = rooms[roomToChangeTo].gameObject;
        foreach (Transform child in activeRoom.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
