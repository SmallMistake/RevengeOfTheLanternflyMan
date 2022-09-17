using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkScript : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    private bool inRange = false;
    public Animator dialogueBubbleAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            dialogueBubbleAnimator.SetBool("InRange", true);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Primary") && inRange)
        {
            dialogueTrigger.TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            dialogueBubbleAnimator.SetBool("InRange", false);
        }
    }
}
