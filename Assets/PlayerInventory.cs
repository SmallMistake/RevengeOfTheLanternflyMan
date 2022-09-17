using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int numberOfAcorns;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddAcorn(int amount)
    {
        numberOfAcorns += amount;
    }
}
