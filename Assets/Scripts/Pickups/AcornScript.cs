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
            InventoryController inventory = collision.gameObject.GetComponent<InventoryController>();
            if (inventory)
            {
                //inventory.AddAcorn(1);
                comboSFXHelper.handleIncrement();
                gameObject.SetActive(false);
            }
        }
    }
}
