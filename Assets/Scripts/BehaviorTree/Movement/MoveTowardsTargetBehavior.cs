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
    public AIBrain aiBrain;
    public AIActionPathfinderToTarget2D pathfindingAction;
    public SharedTransform target;

    private bool reachedDestination;

    public override void OnStart()
    {
        reachedDestination = false;
        aiBrain = GetComponent<AIBrain>();
        pathfindingAction = GetComponent<AIActionPathfinderToTarget2D>();
        pathfindingAction.onDestinationReached.AddListener(HandleDestinationReached);
        aiBrain.Target = target.Value;
        pathfindingAction.Initialization();
    }

    public override TaskStatus OnUpdate()
    {
        pathfindingAction.PerformAction();
        if (reachedDestination)
        {
            pathfindingAction.OnExitState();
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }

    private void HandleDestinationReached()
    {
        pathfindingAction.onDestinationReached.RemoveListener(HandleDestinationReached);
        reachedDestination = true;
    }
}
