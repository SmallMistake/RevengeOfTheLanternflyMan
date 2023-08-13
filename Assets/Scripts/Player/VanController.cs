using MoreMountains.TopDownEngine;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VanController : MonoBehaviour
{
    public string characterName = "PF_Character_Player";
    private Character playerRiding;
    public CharacterMovement vanMovementController;
    public Transform playerStandOnPoint;

    public void JumpOn()
    {
        vanMovementController.PermitAbility(true);
        playerRiding = FindObjectsOfType<Character>().Where(character => character.name == characterName).First() ?? null;
        playerRiding.GetComponent<CharacterMovement>().PermitAbility(false);
        playerRiding.GetComponent<Rigidbody2D>().simulated = false;
        playerRiding.transform.SetParent(playerStandOnPoint);
        playerRiding.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void JumpOff()
    {
        vanMovementController.PermitAbility(true);
    }
}
