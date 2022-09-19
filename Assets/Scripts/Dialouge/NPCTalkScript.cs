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
        PlayerInteractionController interactionController = collision.GetComponent<PlayerInteractionController>();
        if (interactionController)
        {
            inRange = true;
            interactionController.AddDialougeTriggerEntered(dialogueTrigger);
            dialogueBubbleAnimator.SetBool("InRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteractionController interactionController = collision.GetComponent<PlayerInteractionController>();
        if (interactionController)
        {
            interactionController.DialougeTriggerExited(dialogueTrigger);
            inRange = false;
            dialogueBubbleAnimator.SetBool("InRange", false);
        }
    }
}
