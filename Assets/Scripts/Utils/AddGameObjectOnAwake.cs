using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGameObjectOnAwake : MonoBehaviour
{
    public List<GameObject> gameObjectsToAdd = new List<GameObject>();
    private void Awake()
    {
        foreach(GameObject gameObjectToAdd in gameObjectsToAdd)
        {
            Instantiate(gameObjectToAdd);
        }
    }
}
