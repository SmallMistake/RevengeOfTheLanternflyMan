using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LockableDoorTileMapScript : MonoBehaviour
{
    public bool unlockWithKey = true;

    public List<GameObject> switches;
    private List<ISwitch> ISwitches = new List<ISwitch>();

    private void Start()
    {
        if (switches.Count > 0)
        {
            foreach (GameObject switchObject in switches)
            {
                ISwitches.Add(switchObject.GetComponent<ISwitch>());
            }
        }
    }

    private void Update()
    {
        if(switches.Count > 0)
        {
            bool allSwitched = true;
            foreach (ISwitch switchObject in ISwitches)
            {
                if (switchObject.pressed == false)
                {
                    allSwitched = false;
                    break;
                }
            }
            if (allSwitched)
            {
                Unlock();
            }
        }
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
