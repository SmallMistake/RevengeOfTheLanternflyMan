using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestPlayerDataEventOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SaveSystemGameObject>().LoadPlayer();
    }
}
