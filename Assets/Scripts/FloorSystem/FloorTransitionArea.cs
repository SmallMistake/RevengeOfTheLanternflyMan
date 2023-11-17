using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is added to trigger areas that can change the floor of the object that enters it.
/// </summary>
public class FloorTransitionArea : MonoBehaviour
{
    [Tooltip("The floor to change to when the object enters this area")]
    public int floorToGoTo;

    /// <summary>
    /// On Enter, call the objects Floor Switcher controller and tell it to enter this area.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FloorSwitcherController controller = collision.gameObject.GetComponent<FloorSwitcherController>();
        if(controller != null)
        {
            controller.EnterTransitionArea(this);
        }
    }

    /// <summary>
    /// On Enter, call the objects Floor Switcher controller and tell it to exit this area.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        FloorSwitcherController controller = collision.gameObject.GetComponent<FloorSwitcherController>();
        if (controller != null)
        {
            controller.ExitTransitionArea(this);
        }
    }
}
