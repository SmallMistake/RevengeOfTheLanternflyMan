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
    GameObject virtualCamera;


    private void Start()
    {
        if(roomBounds == null) {
            print("Please add room bounds to room " + gameObject.name);
        }
        virtualCamera = (GameObject)Instantiate(Resources.Load("Utilities/PF_2DVirtualCamera_Utility"));
        virtualCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = roomBounds;
        virtualCamera.transform.parent = transform;
        ExitRoom();
    }

    public void EnterRoom()
    {
        virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
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
