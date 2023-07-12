using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public List<Utils.PermanentUpgrades> itemsThatDestroyBlock;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerItemController playerItemController = collision.GetComponent<PlayerItemController>();
        if (playerItemController)
        {
            if(itemsThatDestroyBlock.Contains(playerItemController.itemName))
            {
                Destroy(gameObject);
            }
        }
    }
}
