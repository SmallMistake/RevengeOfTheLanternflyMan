using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Pathfinding;
using System.Drawing;
using UnityEngine;

public class MoveTowardsTargetBehavior : Action
{
    public AIBrain aiBrain;
    public AIActionPathfinderToTarget2D pathfindingAction;
    public SharedTransform target;


    public override void OnStart()
    {
        aiBrain = GetComponent<AIBrain>();
        pathfindingAction = GetComponent<AIActionPathfinderToTarget2D>();
        aiBrain.Target = target.Value;
        pathfindingAction.Initialization();
    }

    public override TaskStatus OnUpdate()
    {
        pathfindingAction.PerformAction();
        if (IsWithinRangeOfDestination())
        {
            //aiDestinationSetter.target = null;
            pathfindingAction.OnExitState();
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }

    private bool IsWithinRangeOfDestination()
    {
        if(false)
        //if(Vector3.Distance(targetMovement.transform.position, transform.position) <= rangeOfSuccess)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
