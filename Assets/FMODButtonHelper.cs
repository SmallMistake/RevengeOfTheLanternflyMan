using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODButtonHelper : MonoBehaviour
{
    public void PlaySelect()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Select");
    }

    public void PlayBack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/BackUI");
    }

    public void PlayHover()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/HoverUI");
    }
}
