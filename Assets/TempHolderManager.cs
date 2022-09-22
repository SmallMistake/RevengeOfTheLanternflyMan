using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempHolderManager : MonoBehaviour
{
    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
