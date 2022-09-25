using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    private Animator interactableAnimator;
    public GameObject textToShow;
    private bool inRange = false;

    public static event Action<GameObject> SignInteractedWith;

    private void Start()
    {
        interactableAnimator = GetComponent<Animator>();
    }
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
        if (Input.GetButtonDown("Primary") && inRange && Time.timeScale == 1)
        {
            SignInteractedWith.Invoke(textToShow);
        }
    }

}
