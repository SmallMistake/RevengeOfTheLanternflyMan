using BehaviorDesigner.Runtime.Tasks;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskIcon("Assets/Art/BehaviorTree/attack-icon.png")]
[TaskName("Is Target In Range")]
[TaskCategory("Conditionals")]
[TaskDescription("Succeed if target is in range")]
public class IsTargetInRange : Conditional
{
    public AIDecisionDetectTargetRadius2D targetRadius2D;

    public AIBrain aiBrain;

    public override TaskStatus OnUpdate()
    {
        targetRadius2D.Decide();
        if(aiBrain.Target != null)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
