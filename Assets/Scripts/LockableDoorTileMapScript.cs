using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LockableDoorTileMapScript : MonoBehaviour, Triggerable
{
    public bool unlockWithKey = true;

    public void trigger()
    {
        Unlock();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && unlockWithKey)
        {
            PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>();
            if(inventory.GetKeys() > 0)
            {
                inventory.AddKey(-1);
                Unlock();
            }
        }
    }

    private void Unlock()
    {
        GameObject.Destroy(gameObject);
    }
}
