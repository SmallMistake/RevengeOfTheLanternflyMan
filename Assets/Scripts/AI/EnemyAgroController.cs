using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAgroController : MonoBehaviour
{
    public ObjectLineOfSightNotifier objectNotifier;

    public List<string> tagsToWatchFor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagsToWatchFor.Contains(collision.tag))
        {
            objectNotifier.StartObjectMovement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tagsToWatchFor.Contains(collision.tag))
        {
            objectNotifier.StopObjectMovement();
        }
    }

}
