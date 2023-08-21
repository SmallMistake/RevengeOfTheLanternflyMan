using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTransitionArea : MonoBehaviour
{
    public int floorToGoTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FloorSwitcherController controller = collision.gameObject.GetComponent<FloorSwitcherController>();
        if(controller != null)
        {
            controller.EnterTransitionArea(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FloorSwitcherController controller = collision.gameObject.GetComponent<FloorSwitcherController>();
        if (controller != null)
        {
            controller.ExitTransitionArea(this);
        }
    }
}
