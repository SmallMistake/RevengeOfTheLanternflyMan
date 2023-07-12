using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseController : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    public FMODUnity.EventReference fmodEvent;


    public GameObject pauseCanvas;
    public TextMeshProUGUI keyText;
    private bool paused = false;

    private bool canPause = true;

    private void Awake()
    {
        PauseInterrupter.ChangeCanPauseStatus += SetCanPause;
        PlayerInventory.keysChanged += UpdateKeyVisual;
    }

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
        print("TODO Fix how audio is handled. Currenly attached to Pause Menu");
    }

    private void SetCanPause(bool canPause)
    {
        this.canPause = canPause;
    }

    private void OnDestroy()
    {
        PlayerInventory.keysChanged -= UpdateKeyVisual;
        PauseInterrupter.ChangeCanPauseStatus -= SetCanPause;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Select") && canPause)
        {
            paused = !paused;
            if (paused) //pause
            {
                instance.setParameterByNameWithLabel("Paused", "True");
                //instance.setParameterByName("Health", 0);
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/PauseStart");
                Time.timeScale = 0;
                pauseCanvas.SetActive(true);
            }
            else //unpause
            {
                instance.setParameterByNameWithLabel("Paused", "False");
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
