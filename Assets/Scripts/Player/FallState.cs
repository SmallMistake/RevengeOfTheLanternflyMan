
using System.Collections;
using UnityEngine;

namespace GBJam.Player
{
	public class FallState : State
	{
		float timePassedWhileFalling = 0;

		public FallState(PlayerStateMachine stateMachine) : base(stateMachine) {
			stateMachine.fallScript.active = false;
			stateMachine.playerInteractionController.enabled = false;
			timePassedWhileFalling = 0;
			stateMachine.playerRigidbody.velocity = Vector3.zero;
			stateMachine.playerHealth.DealDamage(1);
		}

		override
		public void FixedUpdate()
		{
			timePassedWhileFalling += Time.deltaTime * 1.5f;
			stateMachine.transform.localScale = Vector3.Lerp(stateMachine.transform.localScale, Vector3.zero, timePassedWhileFalling);
			if(stateMachine.transform.localScale.x < 0.1)
            {
				stateMachine.respawnAtSSpot();
            }
		}

		override
		public string GetStateName()
        {
			return "Fall";
        }
	}
}
