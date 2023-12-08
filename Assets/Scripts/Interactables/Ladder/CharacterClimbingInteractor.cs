using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When added to trigger areas it will keep track of the climbable objects in range.
/// </summary>
public class CharacterClimbingInteractor : MonoBehaviour
{

    [SerializeField]
    [Tooltip("List of climbable objects in range")]
    private List<ClimbablePoint> climbablePointsInRange = new List<ClimbablePoint>();


    /// <summary>
    /// Returns the first climbable object in range. If there are none, returns null.
    /// </summary>
    public ClimbablePoint GetClimbableInRange()
    {
        if (climbablePointsInRange.Count > 0)
        {
            return climbablePointsInRange[0];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// When a climbable object enters the players range, add it to the list of climbable objects in range.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ClimbablePoint climbable = collision.gameObject.GetComponent<ClimbablePoint>();
        if(climbable != null)
        {
            if (!climbablePointsInRange.Contains(climbable))
            {
                climbablePointsInRange.Add(climbable);
            }
        }
    }

    /// <summary>
    /// When a climbable object exits the players range, remove it from the list of climbable objects in range.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        ClimbablePoint climbable = collision.gameObject.GetComponent<ClimbablePoint>();
        if (climbable != null)
        {
            if (climbablePointsInRange.Contains(climbable))
            {
                climbablePointsInRange.Remove(climbable);
            }
        }
    }
}
