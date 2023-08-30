using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Pathfinding;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

[TaskIcon("Assets/Art/BehaviorTree/walk-icon.png")]
[TaskName("Move Towards Target")]
[TaskCategory("Movement")]
[TaskDescription("Move towards target using A*")]
public class MoveTowardsTargetBehavior : Action
{
    public PathfindingBrain pathfindingBrain;
    public SharedTransform target;

    private bool reachedDestination;

    public override void OnStart()
    {
        reachedDestination = false;
        pathfindingBrain = GetComponent<PathfindingBrain>();
        pathfindingBrain.OnDestinationReached.AddListener(HandleDestinationReached);
        pathfindingBrain.SetNewDestination(target.Value);
        pathfindingBrain.Initialization();
    }

    public override TaskStatus OnUpdate()
    {
        pathfindingBrain.PerformAction();
        if (reachedDestination)
        {
            pathfindingBrain.OnExitState();
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }

    private void HandleDestinationReached()
    {
        pathfindingBrain.OnDestinationReached.RemoveListener(HandleDestinationReached);
        reachedDestination = true;
    }
}
