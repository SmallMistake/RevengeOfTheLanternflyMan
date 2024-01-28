using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// This class controls an image and updates it to match the correct button visual for the controller the player is using
/// </summary>
public class InputButtonVisualController : MonoBehaviour
{
    [SerializeField]
    private Image imageToControl;
    [SerializeField]
    //Ex) Player1_Shoot
    private string controlAxisToMatch;

    private void OnEnable()
    {
        print("TODO Match control axis to normal axis");
    }
}
