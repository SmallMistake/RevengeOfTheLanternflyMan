using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : ISwitch
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FallScript>() || collision.GetComponent<IProjectile>())
        {
            TriggerSwitch();
            SetSprite();
        }
    }
}
