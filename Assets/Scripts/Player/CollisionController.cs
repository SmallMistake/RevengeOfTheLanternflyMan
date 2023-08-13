using MoreMountains.Feedbacks;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoreMountains.TopDownEngine.CharacterStates;

public class CollisionController : MonoBehaviour
{
    public MMFeedbacks collisionFeedback;
    public Character character;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(character.MovementState.CurrentState == MovementStates.Running)
        {
            collisionFeedback.PlayFeedbacks();
        }
    }
}
