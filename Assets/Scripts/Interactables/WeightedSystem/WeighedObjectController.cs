using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script is added to objects that you want to be able to weigh down weighted switches.
/// Only contains data
/// </summary>
public class WeighedObjectController : MonoBehaviour
{
    [Tooltip("Weight of the object")]
    public float weight;

    private Rigidbody2D rb;

    private bool beingMoved = false;

    [Header("Feedbacks")]
    [SerializeField]
    private MMF_Player movementStartedFeedbacks;
    [SerializeField]
    private MMF_Player movementStoppedFeedbacks;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(rb.velocity == Vector2.zero)
        {
            if (beingMoved)
            {
                movementStoppedFeedbacks?.PlayFeedbacks();
                beingMoved = false;
            }
        }
        else
        {
            if (!beingMoved)
            {
                movementStartedFeedbacks?.PlayFeedbacks();
                beingMoved = true;
            } 
        }
    }
}
