using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behaviors {Seek, Flee, Wander, Evade, Pursuit }
[Serializable]
public class ISteeringBehavior
{
    public Behaviors behaviorType;
    public Transform target;

    public ISteeringBehavior(Behaviors behaviorType, Transform target)
    {
        this.behaviorType = behaviorType;
        this.target = target;

    }
}
