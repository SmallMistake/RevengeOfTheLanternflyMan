using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    public GameObject objectToTeleport;

    public GameObject getObjectToTeleport()
    {
        return objectToTeleport;
    }
}