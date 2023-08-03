using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomLock : MonoBehaviour
{
    public UnityEvent onLock;
    public UnityEvent onUnlock;

    public void Lock()
    {
        onLock?.Invoke();
    }

    public void Unlock()
    {
        onUnlock?.Invoke();
    }
}
