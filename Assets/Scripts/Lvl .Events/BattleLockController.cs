using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Call to lock rooms during battles
public class BattleLockController : MonoBehaviour
{
    public GameObject battleLock;

    private void Awake()
    {
        battleLock.SetActive(false);
    }

    public void LockRooms()
    {
        battleLock.SetActive(true);
    }

    public void UnlockRooms()
    {
        battleLock.SetActive(false);
    }
}
