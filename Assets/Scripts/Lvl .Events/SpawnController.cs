using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public void SpawnObject(GameObject spawnObject)
    {
        Instantiate(spawnObject);
    }
}
