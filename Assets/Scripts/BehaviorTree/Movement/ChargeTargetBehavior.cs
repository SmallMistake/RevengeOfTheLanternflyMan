using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskIcon("Assets/Art/BehaviorTree/attack-icon.png")]
[TaskName("Charge Target")]
[TaskCategory("Attack")]
[TaskDescription("Charge Target")]
public class ChargeTargetBehavior : Action
{
    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
