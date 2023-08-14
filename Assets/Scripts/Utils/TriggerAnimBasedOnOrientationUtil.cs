using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimBasedOnOrientationUtil : MonoBehaviour
{
    CharacterOrientation2D characterOrientation;
    public Animator animator;

    public string leftTrigger = "FaceLeft";
    public string rightTrigger = "FaceRight";
    public string upTrigger = "FaceUp";
    public string downTrigger = "FaceDown";

    private Character.FacingDirections currentDirection;


    void Awake()
    {
        characterOrientation = GetComponent<CharacterOrientation2D>();
        currentDirection = characterOrientation.CurrentFacingDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDirection != characterOrientation.CurrentFacingDirection)
        {
            currentDirection = characterOrientation.CurrentFacingDirection;
            switch (currentDirection) {
                case Character.FacingDirections.North:
                    animator.SetTrigger(upTrigger);
                    break;
                case Character.FacingDirections.West:
                    animator.SetTrigger(leftTrigger);
                    break;
                case Character.FacingDirections.East:
                    animator.SetTrigger(rightTrigger);
                    break;
                case Character.FacingDirections.South:
                    animator.SetTrigger(downTrigger);
                    break;
            }
        }
    }
}
