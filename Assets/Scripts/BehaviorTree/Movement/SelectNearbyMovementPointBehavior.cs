using BehaviorDesigner.Runtime.Tasks;
using Pathfinding;
using UnityEngine;

public class SelectNearbyMovementPointBehavior : Action
{
    public float radiusToLook;
    public AIPath ai;

    private bool selectedNewDestination;

    public override void OnStart()
    {
        ai = GetComponent<AIPath>();
        selectedNewDestination = false;
        LookForMovementPoint();
    }

    void LookForMovementPoint()
    {
        ai.destination = PickRandomPoint();
        selectedNewDestination = true;
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * radiusToLook;
        point.z = 0;
        point += ai.position;

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
        if (ai.reachedEndOfPath && selectedNewDestination )
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }
}
