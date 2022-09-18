using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LockableDoorTileMapScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>();
            if(inventory.GetKeys() > 0)
            {
                inventory.AddKey(-1);
                GameObject.Destroy(gameObject);
            }
        }
    }
}
