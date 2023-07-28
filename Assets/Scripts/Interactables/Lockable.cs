using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lockable : MonoBehaviour
{
    public bool unlocked;

    public UnityEvent onUnlock;

    public bool TryToUnlock()
    {
        if (!unlocked)
        {
            unlocked = !unlocked;
            onUnlock?.Invoke();
        }
        return unlocked;
    }
}
