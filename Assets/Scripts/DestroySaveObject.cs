using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySaveObject : MonoBehaviour
{
    private void Awake()
    {
        SaveSystemGameObject saveObject = FindObjectOfType<SaveSystemGameObject>();
        if (saveObject)
        {
            Destroy(saveObject.gameObject);
        }
    }
}
