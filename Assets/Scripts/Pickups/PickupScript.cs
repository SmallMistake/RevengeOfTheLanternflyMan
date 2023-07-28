using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupScript : MonoBehaviour
{
    public InventoryEntry itemData;

    public UnityEvent onItemPickedUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InventoryController playerInventory = collision.GetComponent<InventoryController>();
        if(playerInventory)
        {
            playerInventory.PickupItem(itemData);
            onItemPickedUp?.Invoke();
        }
    }
}
