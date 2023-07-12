using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickup : MonoBehaviour
{
    public Utils.PermanentUpgrades upgradeName;
    public string pickupMessage;
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
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
            Destroy(gameObject);
        }
    }

}
