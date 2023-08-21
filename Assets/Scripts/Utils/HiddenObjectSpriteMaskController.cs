using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObjectSpriteMaskController : MonoBehaviour
{
    public Transform gameObectToFollow;

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if(gameObectToFollow != null)
        {
            transform.position = gameObectToFollow.position + offset;
        }
    }
}
