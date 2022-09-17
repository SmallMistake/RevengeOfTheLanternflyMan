using System.Collections;
using UnityEngine;

namespace GBJam.Player
{
	public class RespawnState : State
	{ 
		public RespawnState(PlayerStateMachine stateMachine) : base(stateMachine) { }

		override
		public IEnumerator Start()
		{
			stateMachine.transform.localScale = new Vector3(0.7f, 0.7f, 1);
			stateMachine.transform.position = stateMachine.lastPositionOnSolidGround;
			stateMachine.setToRegularState();
			yield break;
		}

		override
		public void FixedUpdate()
		{
			Debug.Log("Regular State");
		}
	}
}
