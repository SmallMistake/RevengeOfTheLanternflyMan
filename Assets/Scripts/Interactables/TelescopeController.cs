using Cinemachine;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When interacted with switch to a new camera that represents what the telescope is looking at.
/// </summary>
public class TelescopeController : MonoBehaviour
{
    [Tooltip("The Camera that represents what the telescope is looking at")]
    public CinemachineVirtualCamera virtualCamera;

    public MMF_Player zoomInFeedbacks;
    public MMF_Player zoomOutFeedbacks;

    private void OnEnable()
    {
        virtualCamera.gameObject.SetActive(false);
    }

    /// <summary>
    /// Switch to telescope virtalCamera
    /// </summary>
    public void StartTelescopeMode()
    {
        if (virtualCamera.gameObject.activeSelf)
        {
            zoomOutFeedbacks.PlayFeedbacks();
            virtualCamera.gameObject.SetActive(false);
        }
        else
        {
            zoomInFeedbacks.PlayFeedbacks();
            virtualCamera.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Switch back to standard camera
    /// </summary>
    public void StopTelescopeMode()
    {
        if (virtualCamera.gameObject.activeSelf)
        {
            zoomOutFeedbacks.PlayFeedbacks();
            virtualCamera.gameObject.SetActive(false);
        }
    }
}
