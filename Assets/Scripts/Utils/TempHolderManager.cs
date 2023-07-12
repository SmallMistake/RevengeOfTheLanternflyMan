using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempHolderManager : MonoBehaviour
{

    public void ClearTempHolder()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
