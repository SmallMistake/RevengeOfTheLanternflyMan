using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskIcon("Assets/Art/BehaviorTree/target-icon.jpg")]
[TaskName("Target Player")]
[TaskCategory("Target")]
[TaskDescription("Set Behaviour Tree Target transform to player")]
public class TargetPlayerBehavior : Action
{
    public override void OnStart()
    {
        BehaviorTree behaviorTree = GetComponent<BehaviorTree>();
        SharedTransform sharedTransform = new SharedTransform();
        sharedTransform.Value = GameObject.FindGameObjectWithTag("Player").transform;
        behaviorTree.SetVariable("Target", sharedTransform);
    }
}
