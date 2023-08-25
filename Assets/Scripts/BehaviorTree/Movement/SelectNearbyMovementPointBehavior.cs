using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Pathfinding;
using System.Drawing;
using UnityEngine;

public class SelectNearbyMovementPointBehavior : Action
{
    public float radiusToLook;
    public AIBrain aiBrain;
    public AIActionPathfinderToTarget2D pathfindingAction;

    private bool selectedNewDestination;

    private GameObject targetMovement;
    public float rangeOfSuccess;


    public override void OnStart()
    {
        aiBrain = GetComponent<AIBrain>();
        pathfindingAction = GetComponent<AIActionPathfinderToTarget2D>();
        pathfindingAction.Initialization();
        selectedNewDestination = false;
        LookForMovementPoint();
    }


    void LookForMovementPoint()
    {
        Vector3 destination = PickRandomPoint();
        targetMovement = new GameObject($"{gameObject.name} Destination");
        targetMovement.transform.position = destination;
        aiBrain.Target = targetMovement.transform;
        //aiDestinationSetter.target = targetMovement.transform;
        //ai.SetPath(point);
        //aiDestinationSetter.target = targetMovement.transform;
        selectedNewDestination = true;
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * radiusToLook;
        point.z = 0;
        point += gameObject.transform.position;

        GraphNode node1 = AstarPath.active.GetNearest(point, NNConstraint.Default).node;
        GraphNode node2 = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;
        if (PathUtilities.IsPathPossible(node1, node2))
        {
            return point;
        }
        else
        {
            return PickRandomPoint();
        }
    }

    public override TaskStatus OnUpdate()
    {
        pathfindingAction.PerformAction();
        if (IsWithinRangeOfDestination() && selectedNewDestination)
        {
            //aiDestinationSetter.target = null;
            pathfindingAction.OnExitState();
            GameObject.Destroy(targetMovement);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }

    private bool IsWithinRangeOfDestination()
    {
        if(Vector3.Distance(targetMovement.transform.position, transform.position) <= rangeOfSuccess)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
