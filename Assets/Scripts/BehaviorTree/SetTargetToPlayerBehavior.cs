using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetToPlayerBehavior : Action
{
    public override void OnStart()
    {
        BehaviorTree behaviorTree = GetComponent<BehaviorTree>();
        SharedTransform sharedTransform = new SharedTransform();
        sharedTransform.Value = GameObject.FindGameObjectWithTag("Player").transform;
        //SharedTransform sharedTransform = new SharedTransform.(GameObject.FindGameObjectWithTag("Player").transform);
        behaviorTree.SetVariable("Target", sharedTransform);
    }
}
