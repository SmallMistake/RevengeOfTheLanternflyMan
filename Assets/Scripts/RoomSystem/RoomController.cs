using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{

    public Collider2D roomBounds;
    [SerializeField]
    private List<GameObject> gameObjectsToNotControl = new List<GameObject>();
    public UnityEvent onRoomEnter;
    public UnityEvent onRoomExit;
    GameObject virtualCamera;

    private bool isRoomActive = false;


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
        isRoomActive = true;
        virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
        //Turn on Children
        ChangeGameObjectStatuses(true);
        onRoomEnter?.Invoke();
    }

    public void ExitRoom()
    {
        isRoomActive = false;
        onRoomExit?.Invoke();

        //Turn off Children
        ChangeGameObjectStatuses(false);
    }

    /// <summary>
    /// Turn on or off child game objects unless otherwise specified
    /// </summary>
    /// <param name="newStatus"></param>
    private void ChangeGameObjectStatuses(bool newStatus)
    {
        foreach (Transform child in transform)
        {
            if (gameObjectsToNotControl.Contains(child.gameObject))
            {
                continue;
            }
            else
            {
                child.gameObject.SetActive(newStatus);
            }
        }
    }

    public bool IsRoomActive()
    {
        return isRoomActive;
    }
}
