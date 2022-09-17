
using System.Collections;
using UnityEngine;

namespace GBJam.Player
{
	public class FallState : State
	{ 
		public FallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

		float timePassedWhileFalling = 0;

		override
		public IEnumerator Start()
		{
			stateMachine.fallScript.active = false;
			timePassedWhileFalling = 0;
			stateMachine.playerRigidbody.velocity = Vector3.zero;
			yield break;
		}

		override
		public void FixedUpdate()
		{
			timePassedWhileFalling += Time.deltaTime * 1.5f;
			stateMachine.transform.localScale = Vector3.Lerp(stateMachine.transform.localScale, Vector3.zero, timePassedWhileFalling);
			if(stateMachine.transform.localScale.x == 0)
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
