using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTrigger : MoveBetweenPoints, Triggerable
{
    private bool triggered = false;

    private new void Awake()
    {
        base.Awake();
        currentDestination = 0;
        movingObject.transform.position = destinations[currentDestination].position;
        currentDestination++;
    }

    public void trigger()
    {
        triggered = true;
    }

    private void Update()
    {
        if (triggered && currentDestination < destinations.Count)
        {
            if (movingObject.transform.position == destinations[currentDestination].position)
            {
                currentDestination++;
                if(currentDestination >= destinations.Count - 1)
                {
                    stopped = true;
                }
            }
            if (!stopped)
            {
                base.HandleMovement();
            }
        }
    }
}
