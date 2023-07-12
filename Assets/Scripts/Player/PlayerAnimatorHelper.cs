using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHelper : MonoBehaviour
{
    PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = GetComponentInChildren<PlayerHealth>();
    }

    public void EndIFrames()
    {
        playerHealth.EndIFrames();
    }
}
