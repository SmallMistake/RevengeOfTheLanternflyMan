using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionNotifier : MonoBehaviour
{
    public GameObject transtionOverlay;
    TransitionController controller;
    public delegate void TransitionInReady();
    public static TransitionInReady transitionInReady;

    public void StartTranstion()
    {
        Instantiate(transtionOverlay);
    }
    public void NotifyOfReadyToTranstionIn()
    {
        transitionInReady?.Invoke();
    }
}
