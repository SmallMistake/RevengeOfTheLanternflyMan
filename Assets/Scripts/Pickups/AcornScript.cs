using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornScript : MonoBehaviour
{
    public FMODIncrementingHelper comboSFXHelper;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>();
            if (inventory)
            {
                inventory.AddAcorn(1);
                comboSFXHelper.handleIncrement();
                gameObject.SetActive(false);
            }
        }
    }
}
