using IntronDigital;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionObjectHelper : MonoBehaviour
{
    public void FinishedToBlack()
    {
        TransitionEvent.Trigger(TransitionEventTypes.FinishedToBlack, null);
        StartCoroutine(DestroyTransitionElement());
    }

    public void FinishedOutOfBlack()
    {
        TransitionEvent.Trigger(TransitionEventTypes.FinishedOutOfBlack, null);
        Destroy(gameObject);
    }

    //ToAllowTheObject To WaitBefore Destroying so screen does not blink. 
    IEnumerator DestroyTransitionElement()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
