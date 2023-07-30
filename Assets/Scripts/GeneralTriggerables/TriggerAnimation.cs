using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour,Triggerable
{
    public Animator animator;
    public string triggerToSetOff;
    public void trigger()
    {
        animator.SetTrigger(triggerToSetOff);
    }
}
