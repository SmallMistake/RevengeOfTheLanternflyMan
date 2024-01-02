using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragType { 
    Horizontal,
    Vertical,
    Free
}


public class Draggable : MonoBehaviour
{
    public Rigidbody2D rigidBodyToAffect;
    public DragType dragType;

    public void StartDragging(Transform newParent)
    {
        rigidBodyToAffect.bodyType = RigidbodyType2D.Kinematic;
        rigidBodyToAffect.transform.parent = newParent;
    }

    public void StopDragging()
    {
        rigidBodyToAffect.bodyType = RigidbodyType2D.Dynamic;
        rigidBodyToAffect.transform.parent = null;
    }
}
