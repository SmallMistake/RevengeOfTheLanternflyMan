using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectLineOfSightNotifier: MonoBehaviour
{

    public abstract void StartObjectMovement();

    public abstract void StopObjectMovement();
}
