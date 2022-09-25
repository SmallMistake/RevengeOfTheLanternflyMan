using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPathLineOfSightController : ObjectLineOfSightNotifier
{
    private AIPath aiPath;

    private void OnEnable()
    {
        aiPath.canMove = false;
    }

    void Awake()
    {
        aiPath = GetComponent<AIPath>();
    }

    public override void StartObjectMovement()
    {
        aiPath.canMove = true;
    }

    public override void StopObjectMovement()
    {
        //I don't want the enemy to stop chasing once the player is in their sights
        //aiPath.canMove = false;
    }
}
