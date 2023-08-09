using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCreditsOnBossDialougeFinish : MonoBehaviour
{
    //Quick and dirty class destroy and replace with proper class when have time.
    Triggerable animationTrigger;

    void Start()
    {
        animationTrigger = GetComponent<Triggerable>();
        DialogueTrigger.DialougeEnded += TriggerCredits;
    }

    private void OnDestroy()
    {
        DialogueTrigger.DialougeEnded -= TriggerCredits;
    }

    private void TriggerCredits(string f)
    {
        animationTrigger.trigger();
        Time.timeScale = 0;
    }
}
