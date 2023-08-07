using System.Collections;
using UnityEngine;

public class OpeningCutsceneController : MonoBehaviour
{
    private Animator animator;
    private string triggerFilter = "OpeningCutscene";
    private LevelLoader levelLoader;

    private void Start()
    {
        animator = GetComponent<Animator>();
        levelLoader = gameObject.GetComponent<LevelLoader>();
        DialogueTrigger.DialougeEnded += FinishCutscene;
    }

    private void OnDestroy()
    {
        DialogueTrigger.DialougeEnded -= FinishCutscene;
    }

    public void TriggerAnimation(string animationTrigger)
    {
        animator.SetTrigger(animationTrigger);
    }

    private void FinishCutscene(string triggerName)
    {
        if (triggerName.Equals(triggerFilter))
        {
            StartCoroutine(WaitTillReadyToTransition());
        }
    }

    IEnumerator WaitTillReadyToTransition()
    {
        yield return new WaitForSeconds(2);
        levelLoader.LoadLevel("GreenMeadow");
    }
}
