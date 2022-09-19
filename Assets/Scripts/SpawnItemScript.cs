using UnityEngine;
using System.Collections;

public class SpawnItemScript : MonoBehaviour, Triggerable
{
    public GameObject itemToSpawn;
    public bool spawnOnce = true;
    bool spawned = false;
    public float spawnOnTimer = 0f;

    private void OnEnable()
    {
        if (spawnOnTimer > 0)
        {
            StartCoroutine(SpawnOnTimer(spawnOnTimer));
        }
    }

    public void trigger()
    {
        if ((!spawned && spawnOnce) || !spawnOnce)
        {
            GameObject key = spawnItem();
            Animator animator = key.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("FallFromTop");
            }
            spawned = true;
        }
    }

    IEnumerator SpawnOnTimer(float interval)
    {
        while (true)
        {
            spawnItem();
            print(interval);
            yield return new WaitForSeconds(interval);
            print("Looped");
        }
    }

    private GameObject spawnItem()
    {
        return Instantiate(itemToSpawn, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }
}
