using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    public Animator animator;
    public SpawnProjectileScript projectileSpawner;
    private DialogueTrigger lastDialogueTriggerEntered;
    internal bool active;

    public static event Action<Utils.PermanentUpgrades?> UsedPrimary; //Used by Observers
    public static event Action<Utils.PermanentUpgrades?> UsedSecondary; //Used by Observers

    private PlayerInventory playerInventory;

    bool primaryOnCooldown = false;
    float primaryCooldown = 0.4f;

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Primary")){

            if (lastDialogueTriggerEntered) //Speak
            {
                lastDialogueTriggerEntered.TriggerDialogue();
            }
            else if(!primaryOnCooldown) //Attack
            {
                if (playerInventory.UnlockedItem(Utils.PermanentUpgrades.Pecticide))
                {
                    animator.SetTrigger("Primary");
                    UsedPrimary.Invoke(Utils.PermanentUpgrades.Pecticide);
                    StartCoroutine(HandlePrimaryCooldown());
                }
                else
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/FailedToUseItem");
                    UsedPrimary.Invoke(null);
                }
            }
        }
        if (Input.GetButtonDown("Secondary"))
        {
            if (playerInventory.UnlockedItem(Utils.PermanentUpgrades.Walnut))
            {
                if (playerInventory.UseWalnut())
                {
                    projectileSpawner.SpawnAtLocation();
                }
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/FailedToUseItem");
                UsedSecondary.Invoke(null);
            }
        }
    }

    public void AddDialougeTriggerEntered(DialogueTrigger dialogueTrigger)
    {
        lastDialogueTriggerEntered = dialogueTrigger;
    }

    public void DialougeTriggerExited(DialogueTrigger dialogueTrigger)
    {
        if(lastDialogueTriggerEntered == dialogueTrigger)
        {
            lastDialogueTriggerEntered = null;
        }
    }

    IEnumerator HandlePrimaryCooldown()
    {
        primaryOnCooldown = true;
        yield return new WaitForSeconds(primaryCooldown);
        primaryOnCooldown = false;
    }
}
