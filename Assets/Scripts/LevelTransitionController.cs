using UnityEngine;

public class LevelTransitionController : MonoBehaviour
{
    private Animator transitionAnimator;
    public GameObject canvasGroup;
    internal bool transitionFinished = false;

    private void Start()
    {
        Time.timeScale = 1;
    }
}
