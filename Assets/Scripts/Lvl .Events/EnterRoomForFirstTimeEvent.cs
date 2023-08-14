using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterRoomForFirstTimeEvent : MonoBehaviour
{
    public bool enteredRoom;

    public UnityEvent onEnter;

    public void OnEnter()
    {
        if (!enteredRoom)
        {
            enteredRoom = true;
            onEnter?.Invoke();
        }
    }
}
