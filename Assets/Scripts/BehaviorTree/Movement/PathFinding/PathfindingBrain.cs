using UnityEngine;
using MoreMountains.Tools;
using Pathfinding;
using UnityEngine.AI;
using System;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// This class can be used to create a path towards a destination
/// </summary>
[MMHiddenProperties("AbilityStartFeedbacks", "AbilityStopFeedbacks")]
[AddComponentMenu("Intron Digital/Character/Pathfinding Brain")]
public class PathfindingBrain : MonoBehaviour
{

    #region Properties

    [Header("Steering")]
    [Tooltip("What is is updated with the path points to steer the object")]
    public SteeringMasterController steeringController;

    [Header("PathfindingTarget")]
    /// the target the character should pathfind to
    [Tooltip("the final target the character should eventually get to")]
    public Transform target;

    [Tooltip("a mid target the character is moving to on it's way to the final target")]
    public Transform waypointTarget;

    /// the distance to waypoint at which the movement is considered complete
    [Tooltip("the distance to waypoint at which the movement is considered complete")]
    public float distanceToWaypointThreshold = 1f;

    /// if the target point can't be reached, the distance threshold around that point in which to look for an alternative end point
    [Tooltip(
        "if the target point can't be reached, the distance threshold around that point in which to look for an alternative end point")]
    public float closestPointThreshold = 3f;

    [Header("Debug")]
    /// whether or not we should draw a debug line to show the current path of the character
    [Tooltip("whether or not we should draw a debug line to show the current path of the character")]
    public bool debugDrawPath;

    /// the current path
    [MMReadOnly]
    [Tooltip("the current path")]
    public NavMeshPath agentPath;

    /// a list of waypoints the character will go through
    [MMReadOnly]
    [Tooltip("a list of waypoints the character will go through")]
    public Vector3[] waypoints;

    /// the index of the next waypoint
    [MMReadOnly]
    [Tooltip("the index of the next waypoint")]
    public int nextWaypointIndex;

    /// the direction of the next waypoint
    [MMReadOnly]
    [Tooltip("the direction of the next waypoint")]
    public Vector3 nextWaypointDirection;

    /// the distance to the next waypoint
    [MMReadOnly]
    [Tooltip("the distance to the next waypoint")]
    public float distanceToNextWaypoint;


    /// the distance to the destination
    [MMReadOnly]
    [Tooltip("the distance to the next waypoint")]
    public float distanceToDestination;

    public event Action<int, int, float> onPathProgress;

    public float timeBetweenPathRebuildsSeconds = 0.1f;

    public UnityEvent OnDestinationReached;

    protected int _waypoints;
    protected Vector3 _direction;
    protected Vector2 _newMovement;
    protected Vector3 _lastValidTargetPosition;
    protected Vector3 _closestNavmeshPosition;
    protected NavMeshHit _navMeshHit;
    protected bool _pathFound;

    private Coroutine rebuildPathRoutine;
    #endregion


    public void Initialization()
    {
        if(steeringController == null)
        {
            steeringController = GetComponent<SteeringMasterController>();
        }
        // AgentPath = new NavMeshPath();
        _lastValidTargetPosition = this.transform.position;
        waypointTarget = new GameObject("PathfindingWaypoint").transform;
        steeringController.AddBehavior(Behaviors.Seek, waypointTarget);
        Array.Resize(ref waypoints, 5);
    }

    /// <summary>
    /// Sets a new destination the character will pathfind to
    /// </summary>
    /// <param name="destinationTransform"></param>
    public virtual void SetNewDestination(Transform destinationTransform)
    {
        if (destinationTransform == null)
        {
            target = null;
            return;
        }

        target = destinationTransform;
        DeterminePath(this.transform.position, destinationTransform.position);

        if (rebuildPathRoutine != null)
        {
            StopCoroutine(rebuildPathRoutine);

        }
        rebuildPathRoutine = StartCoroutine(HandlePathRebuildTimer());
    }

    IEnumerator HandlePathRebuildTimer()
    {
        while(target != null)
        {
            yield return new WaitForSeconds(timeBetweenPathRebuildsSeconds);
            DeterminePath(transform.position, target.position);
        }
        rebuildPathRoutine = null;
    }

    /// <summary>
    /// On Update, we draw the path if needed, determine the next waypoint,
    /// </summary>
    public void PerformAction()
    {
        if (target == null)
        {
            return;
        }
        if (!ReachedDestination())
        {
            waypointTarget.position = waypoints[nextWaypointIndex];
            DrawDebugPath();
            DetermineDistanceToNextWaypoint();
            DetermineDistanceToDestination();
        }
        else
        {
            OnDestinationReached?.Invoke();
            //characterMovement.SetMovement(Vector2.zero);
        }
    }

    public bool ReachedDestination()
    {
        if(nextWaypointIndex >= waypoints.Length)
        {
            return true;
        }
        else {
            return false;
        }
    }

    /// <summary>
    /// Determines the next path position for the agent. NextPosition will be zero if a path couldn't be found
    /// </summary>
    /// <param name="startingPos"></param>
    /// <param name="targetPos"></param>
    /// <returns></returns>        
    protected virtual void DeterminePath(Vector3 startingPosition, Vector3 targetPosition)
    {
        var seeker = GetComponent<Seeker>();
        seeker.StartPath(startingPosition, targetPosition, path =>
        {
            _closestNavmeshPosition = targetPosition;
            waypoints = path.vectorPath.ToArray();
            _waypoints = path.vectorPath.Count;
            nextWaypointIndex = 0;
            while (nextWaypointIndex < waypoints.Length)
            {
                if ((waypoints[nextWaypointIndex] - transform.position).magnitude < distanceToWaypointThreshold)
                {
                    nextWaypointIndex++;
                }
                else
                {
                    break;
                }
            }
            onPathProgress?.Invoke(nextWaypointIndex, waypoints.Length,
                Vector3.Distance(this.transform.position, waypoints[nextWaypointIndex]));
        });

            
    }

    /// <summary>
    /// Determines the next waypoint based on the distance to it
    /// </summary>
    protected virtual void DetermineNextWaypoint()
    {
        if (_waypoints <= 0)
        {
            return;
        }

        if (nextWaypointIndex < 0)
        {
            return;
        }

        var distance = Vector3.Distance(this.transform.position, waypoints[nextWaypointIndex]);
        if (distance <= distanceToWaypointThreshold)
        {
            if (nextWaypointIndex + 1 < _waypoints)
            {
                nextWaypointIndex++;
            }
            else
            {
                nextWaypointIndex = -1;
            }

            onPathProgress?.Invoke(nextWaypointIndex, _waypoints, distance);
        }
    }

    /// <summary>
    /// Determines the distance to the next waypoint
    /// </summary>
    protected virtual void DetermineDistanceToNextWaypoint()
    {
        if (nextWaypointIndex <= 0)
        {
            distanceToNextWaypoint = 0;
        }   
        else
        {
            distanceToNextWaypoint = Vector3.Distance(this.transform.position, waypoints[nextWaypointIndex]);
        }
    }

    /// <summary>
    /// Determines the distance to the next waypoint
    /// </summary>
    protected virtual void DetermineDistanceToDestination()
    {
        if (target == null)
        {
            distanceToNextWaypoint = -999;
        }
        else
        {
            distanceToDestination = Vector3.Distance(this.transform.position, target.transform.position);
        }
    }

    public void OnExitState()
    {
        StopCoroutine(rebuildPathRoutine);
        rebuildPathRoutine = null;
        Destroy(target.gameObject);
        target = null;
        Destroy(waypointTarget.gameObject);
        waypointTarget = null;
        steeringController.RemoveBehavior(Behaviors.Seek, waypointTarget);
    }

    /// <summary>
    /// Draws a debug line to show the current path
    /// </summary>
    protected virtual void DrawDebugPath()
    {
        if (debugDrawPath)
        {
            if (_waypoints <= 0)
            {
                if (target != null)
                {
                    DeterminePath(transform.position, target.position);
                }
            }

            for (int i = 0; i < _waypoints - 1; i++)
            {
                Debug.DrawLine(waypoints[i], waypoints[i + 1], Color.red);
            }
        }
    }
}