using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectileScript : MonoBehaviour
{
    public GameObject projectile;
    public void SpawnAtLocation()
    {
        projectile.layer = gameObject.layer;
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
