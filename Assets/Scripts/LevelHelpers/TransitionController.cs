using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public delegate void TransitionedOut();
    public static TransitionedOut transitionedOut;

    public Animator animationController;

    private void Awake()
    {
        TransitionNotifier.transitionInReady += TransitionIn;
    }

    private void OnDestroy()
    {
        TransitionNotifier.transitionInReady -= TransitionIn;
    }

    public void TransitionOutFinished()
    {
        transitionedOut?.Invoke();
    }

    //This should be called by what ever caused the transition to get the visuals to transition back in
    public void TransitionIn()
    {
        animationController.SetTrigger("TransitionIn");
    }
}
