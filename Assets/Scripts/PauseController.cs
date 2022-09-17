using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pauseCanvas;
    private bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Select"))
        {
            paused = !paused;
            if (paused) //pause
            {
                Time.timeScale = 0;
                pauseCanvas.SetActive(true);
            }
            else //unpause
            {
                Time.timeScale = 1;
                pauseCanvas.SetActive(false);
            }
        }
    }
}
