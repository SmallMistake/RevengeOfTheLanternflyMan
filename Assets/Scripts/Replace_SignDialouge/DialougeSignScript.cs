using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialougeSignScript : MonoBehaviour
{
    public Animator interactableAnimator;
    public UnityEvent onUseDialougeTrigger;
    private bool inRange = false;

    public static event Action<GameObject> SignInteractedWith;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            interactableAnimator.SetBool("InRange", true);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactableAnimator.SetBool("InRange", false);
            inRange = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Player1_Shoot") && inRange && Time.timeScale == 1)
        {
            onUseDialougeTrigger.Invoke();
        }
    }

}
