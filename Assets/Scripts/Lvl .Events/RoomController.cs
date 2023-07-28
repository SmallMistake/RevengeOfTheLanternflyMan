using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{

    public Collider2D roomBounds;
    public UnityEvent onRoomEnter;
    public UnityEvent onRoomExit;

    private void Awake()
    {
        if(roomBounds == null) {
            print("Please add room bounds to room " + gameObject.name);
        }
        GameObject virtualCamera = (GameObject)Instantiate(Resources.Load("Utilities/2DVirtualCamera"));
        virtualCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = roomBounds;
        virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
        virtualCamera.transform.parent = transform;
        ExitRoom();
    }

    public void EnterRoom()
    {
        //Turn on Children
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        onRoomEnter?.Invoke();
    }

    public void ExitRoom()
    {
        onRoomExit?.Invoke();

        //Turn off Children
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
