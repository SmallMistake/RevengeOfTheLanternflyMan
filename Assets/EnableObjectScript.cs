using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectScript : MonoBehaviour
{
    public GameObject objectToEnable;
    public void enableObject()
    {
        objectToEnable.SetActive(true);
    }
}
