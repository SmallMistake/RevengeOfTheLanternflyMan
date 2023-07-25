using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockable : MonoBehaviour
{
    public bool unlocked;

    public bool TryToUnlock()
    {
        if (!unlocked)
        {
            unlocked = !unlocked;
        }
        return unlocked;
    }
}
