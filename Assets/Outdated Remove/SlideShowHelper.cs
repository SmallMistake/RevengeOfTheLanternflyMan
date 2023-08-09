using UnityEngine;

public class SlideShowHelper : MonoBehaviour
{
    private Animator animator;
    private bool canMoveToNextScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeSlideShowStatus(int status)
    {
        if(status == 1)
        {
            animator.ResetTrigger("MoveToNextSlide");
            canMoveToNextScreen = true;
        } else if (status == 0)
        {
            canMoveToNextScreen = false;
        }
        else
        {
           //TODO throw error
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMoveToNextScreen)
        {
            if (Input.GetButtonDown("Start") || Input.GetButtonDown("Select") || Input.GetButtonDown("Primary"))
            {
                animator.SetTrigger("MoveToNextSlide");
            }
        }
    }
}
