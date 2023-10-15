using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClimbingInteractor : MonoBehaviour
{

    [SerializeField]
    private List<ClimbablePoint> climbablePointsInRange = new List<ClimbablePoint>();


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

    //Catalog objects in range
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
