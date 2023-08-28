using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleBehavior : Action
{
    private bool foundPlayer;
    public override void OnStart()
    {
        foundPlayer = false;
        StartCoroutine(LookForPlayer());
    }

    IEnumerator LookForPlayer()
    {
        yield return new WaitForSeconds(4);
        foundPlayer = true;
    }
    public override TaskStatus OnUpdate()
    {
        foundPlayer = false;
        return foundPlayer ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {

    }
}
