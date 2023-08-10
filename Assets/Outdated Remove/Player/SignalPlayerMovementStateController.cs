using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoreMountains.TopDownEngine.CharacterStates;


//This class is to be used by things like the timeline to allow for player movement to be disabled.
public class SignalPlayerMovementStateController : MonoBehaviour
{
    public void DisablePlayerMovement()
    {
        FindObjectOfType<InputManager>().InputDetectionActive = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<TopDownController2D>().SetMovement(Vector3.zero);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().MovementState.ChangeState(MovementStates.Idle);
        CharacterMovement movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        movementController.AbilityPermitted = false;
    }
    public void EnablePlayerMovement() {
        FindObjectOfType<InputManager>().InputDetectionActive = true;
        CharacterMovement movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        movementController.AbilityPermitted = true;
    }

}
