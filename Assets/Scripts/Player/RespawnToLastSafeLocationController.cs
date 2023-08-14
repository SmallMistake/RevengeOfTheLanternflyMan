using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoreMountains.TopDownEngine.CharacterStates;

public class RespawnToLastSafeLocationController : CharacterAbility
{
    public Vector3 lastSafeRespawnPoint;

    /// <summary>
    /// On process ability, we check for holes
    /// </summary>
    public override void ProcessAbility()
    {
        base.ProcessAbility();
        CheckIfGrounded();
    }

    // Update is called once per frame
    void CheckIfGrounded()
    {
        if (_controller2D.Grounded)
        {
            lastSafeRespawnPoint = transform.position;
        }
    }


    public void Respawn()
    {
        transform.position = lastSafeRespawnPoint;
        _character.MovementState.ChangeState(MovementStates.Idle);
    }
}
