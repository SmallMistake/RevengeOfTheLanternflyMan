using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private void OnEnable()
    {
        virtualCamera.gameObject.SetActive(false);
    }

    public void StartTelescopeMode()
    {
        print("Start Telescope Mode");
        if (virtualCamera.gameObject.activeSelf)
        {
            virtualCamera.gameObject.SetActive(false);
        }
        else
        {
            virtualCamera.gameObject.SetActive(true);
        }
    }

    public void StopTelescopeMode()
    {
        if (virtualCamera.gameObject.activeSelf)
        {
            virtualCamera.gameObject.SetActive(false);
        }
    }
}
