using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSystem : MonoBehaviour
{
    public float rotationSpeed;
    private void Update()
    {
        transform.Rotate(new Vector3(0,0,1), rotationSpeed);
    }
}
