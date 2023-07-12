using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRoomReenter : MonoBehaviour
{
    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalScale;

    Transform child;

    // Start is called before the first frame update
    void Awake()
    {
        child = transform.GetChild(0);
        originalPosition = child.position;
        originalRotation = child.rotation;
        originalScale = child.localScale;
    }

    private void OnEnable()
    {
        child.position = originalPosition;
        child.rotation = originalRotation;
        child.localScale = originalScale;

        child.gameObject.SetActive(true);
    }
}
