using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<FallScript>())
        {
            collision.transform.parent.transform.parent.SetParent(transform); //Bug where if the fall script is turned off it breaks.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<FallScript>())
        {
            collision.transform.parent.transform.parent.SetParent(null);
        }
    }
}
    
