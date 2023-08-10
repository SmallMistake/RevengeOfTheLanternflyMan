
using System.Collections;
using UnityEngine;

namespace GBJam.Player
{
	public class DieState : State
	{
		public DieState(PlayerStateMachine stateMachine) : base(stateMachine) { }

		override
		public IEnumerator Start()
		{

			Time.timeScale = 0;
			stateMachine.fallScript.active = false;
			yield break;
		}
	}
}
