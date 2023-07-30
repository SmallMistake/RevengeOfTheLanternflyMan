using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeBasedScriptedEventController : MonoBehaviour
{
    public List<TimeEvent> events = new List<TimeEvent>();

    private void OnEnable()
    {
        StartCoroutine(HandleTimeline());
    }

    IEnumerator HandleTimeline()
    {
        foreach(TimeEvent timeEvent in events)
        {
            yield return new WaitForSeconds(timeEvent.secondsTillEventPlays);
            timeEvent.onTimeReached.Invoke();
        }
    }
}

[System.Serializable]
public class TimeEvent
{
    public float secondsTillEventPlays;
    public UnityEvent onTimeReached;
}