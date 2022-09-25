using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseController : MonoBehaviour
{
    public GameObject pauseCanvas;
    public TextMeshProUGUI keyText;
    private bool paused = false;

    private void Awake()
    {
        PlayerInventory.keysChanged += UpdateKeyVisual;
    }

    private void OnDestroy()
    {
        PlayerInventory.keysChanged -= UpdateKeyVisual;
    }

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

    private void UpdateKeyVisual(int keys)
    {
        keyText.text = $"Keys: {keys}";
    }
}
