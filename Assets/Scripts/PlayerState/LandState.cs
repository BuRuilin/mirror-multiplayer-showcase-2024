using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// веб╫в╢л╛
/// </summary>
public class LandState : PlayerState
{
	PlayerStateMachine StateMachine;
	GameObject gameObject;
	PlayerController playerController;
	
	Animator animator;
	Rigidbody2D rigidbody2D;
	

	private float originGravity;

	private WaitTime wait = new WaitTime(0.1f);
	public override void Init(PlayerStateMachine stateMachine)
	{
		StateMachine = stateMachine;
		gameObject = stateMachine.gameObject;
		playerController = gameObject.GetComponent<PlayerController>();
		animator = gameObject.GetComponent<Animator>();
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	public override void OnEnterState(string lastState)
	{
		animator.SetBool("isLand", true);
		animator.CrossFade("Land", 0.1f);
		rigidbody2D.gravityScale = 0;
		rigidbody2D.velocity = Vector3.zero;
		wait.Start();
	}
	public override void OnStayState()
	{
		wait.FixedTick();
		if (wait.isFinished)
		{
			if (playerController.direction == 0)
			{
				StateMachine.ChangeState("Idle");
			}
			else
			{
				StateMachine.ChangeState("Run");
			}
		}
	}
	public override void OnExitState()
	{
		wait.Reset();
		animator.SetBool("isLand", false);
		playerController.isJump = false;
	}
}