
using System.Collections;
using UnityEngine;

namespace GBJam.Player
{
	public class RegularState : State
	{
		public RegularState(PlayerStateMachine stateMachine) : base(stateMachine) { }

		override
		public IEnumerator Start()
		{
			stateMachine.fallScript.active = true;
			yield break;
		}

		override
		public void Update()
        {
			if (Input.GetButtonDown("Attack"))
			{
				Debug.Log("TODO Attack");
			}
		}

		override
		public void FixedUpdate()
		{
			stateMachine.horizontalMove = Input.GetAxisRaw("Horizontal") * stateMachine.moveSpeed;
			stateMachine.verticalMove = Input.GetAxisRaw("Vertical") * stateMachine.moveSpeed;

			Vector2 movement = new Vector2(stateMachine.horizontalMove * stateMachine.moveSpeed, stateMachine.verticalMove * stateMachine.moveSpeed);
			movement *= Time.deltaTime;

			// Move our character
			//stateMachine.playerRigidbody.velocity = (movement);
			stateMachine.playerRigidbody.AddForce(movement);

			//Deal with animation
			stateMachine.animator.SetFloat("Vertical Speed", movement.y);
			stateMachine.animator.SetFloat("Horizontal Speed", movement.x);
		}
	}
}
