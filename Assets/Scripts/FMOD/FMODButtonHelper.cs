using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODButtonHelper : MonoBehaviour
{
    public void PlaySelect()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Select");
    }

    public void PlayBack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/BackUI");
    }

    public void PlayHover()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/HoverUI");
    }
}
