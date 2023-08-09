using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNotifier : MonoBehaviour
{
    public Health healthToListenTo;
    public HealthChangeEvent healthChangeEvent;

    public void NotifyOfDamage()
    {
        print("G");
    }
}
