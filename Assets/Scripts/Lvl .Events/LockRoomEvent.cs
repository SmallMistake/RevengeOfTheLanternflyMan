using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockRoomEvent : MonoBehaviour
{
    public bool recurring = false;
    private bool locked = true;

    public UnityEvent onRoomLock;

    public UnityEvent onRoomUnlock;

    public void LockRoom()
    {
        if(recurring || ( !recurring && locked))
        {
            onRoomLock?.Invoke();
        }
    }

    public void UnlockRoom()
    {
        onRoomUnlock?.Invoke();
        locked = false;
    }
}
