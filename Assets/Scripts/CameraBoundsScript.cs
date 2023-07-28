using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBoundsScript : MonoBehaviour
{
    CinemachineConfiner confiner;
    CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = transform.parent.GetComponent<CompositeCollider2D>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
