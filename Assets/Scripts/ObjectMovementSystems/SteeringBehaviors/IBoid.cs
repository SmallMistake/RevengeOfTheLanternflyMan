using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBoid: MonoBehaviour
{
    public float maxVelocity;
    public float movementSpeed;
    public float slowingDistance;
    public TopDownController2D controller;
    private Rigidbody2D rb;

    private void Start()
    {
        if(controller == null)
        {
            controller = GetComponent<TopDownController2D>();
        }
        rb = GetComponent<Rigidbody2D>();
        controller.Velocity = Vector3.zero;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public float GetMass()
    {
        return rb.mass;
    }

    public float GetMaxVelocity()
    {
        return maxVelocity;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
        //return controller.Velocity;
    }
}
