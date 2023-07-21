using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttack : AttackBase
{
    public GameObject objectToSpawn;
    public string spawnedlayerName;

    public override void Attack()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn);
        spawnedObject.transform.position = gameObject.transform.position;
        spawnedObject.transform.rotation = gameObject.transform.rotation;
        if (spawnedlayerName != null)
        {
            spawnedObject.layer = LayerMask.NameToLayer(spawnedlayerName);
        }
    }
}
