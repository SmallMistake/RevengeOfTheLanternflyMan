using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWhenInRange : ObjectLineOfSightNotifier
{
    public float shootInterval;

    List<SpawnProjectileScript> projectileSpawners;
    Coroutine shootCouroutine;
    public Transform target;

    void Awake()
    {

        projectileSpawners = new List<SpawnProjectileScript>(transform.GetComponentsInChildren<SpawnProjectileScript>());
    }


    public override void StartObjectMovement()
    {
        shootCouroutine = StartCoroutine(ShootAtInterval());
    }

    public override void StopObjectMovement()
    {
        if(shootCouroutine != null)
        {
            StopCoroutine(shootCouroutine);
            shootCouroutine = null;
        }
    }

    private void Update()
    {
        if (shootCouroutine != null)
        {
            UpdateObjectRotation();
        }
    }

    void UpdateObjectRotation()
    {
            print(target.position + "    " + transform.position);
            float xDistance = target.position.x - transform.position.x;
            float yDistance = target.position.y - transform.position.y;

            float absoluteDistanceX = Mathf.Abs(xDistance);
            float absoluteDistanceY = Mathf.Abs(yDistance);
            if (xDistance < 0 && absoluteDistanceX > absoluteDistanceY)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (yDistance > 0 && absoluteDistanceX < absoluteDistanceY)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            else if (yDistance < 0 && absoluteDistanceX < absoluteDistanceY)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
    }
    

    IEnumerator ShootAtInterval()
    {
        UpdateObjectRotation();
        while (true)
        {
            foreach (SpawnProjectileScript projectileSpawner in projectileSpawners)
            {
                projectileSpawner.SpawnAtLocation();
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
