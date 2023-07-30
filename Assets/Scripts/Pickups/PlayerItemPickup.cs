using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Remove this or make it into an upgrade Pickup 
public class PlayerItemPickup : MonoBehaviour
{
    public Utils.PermanentUpgrades upgradeName;
    public string pickupMessage;
    private InventoryController playerInventory;
    public GameObject objectToDestroy;

    private void Start()
    {
        playerInventory = FindObjectOfType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interactables/GetNewUpgrade");
            playerInventory.UnlockUpgrade(upgradeName);
            pickupMessage = pickupMessage.Replace("\\n", "\n");
            GeneralUpdateManager updateManager = FindObjectOfType<GeneralUpdateManager>();
            updateManager.ShowNotification(pickupMessage);
            Destroy(objectToDestroy ?? gameObject);
        }
    }

}
