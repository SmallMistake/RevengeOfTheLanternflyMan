using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void Start()
    {
        
    }

    public void TriggerDialogue()
    {
        if(Time.timeScale != 0)
        {
            FindObjectOfType<DialougeManager>().StartDialogue(dialogue);
        }
    }
}
