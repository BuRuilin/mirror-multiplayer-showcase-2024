using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ¾²Ö¹×´Ì¬
/// </summary>
public class IdleState : PlayerState
{
	private PlayerStateMachine StateMachine;
	private GameObject gameObject;
	private PlayerController playerController;
	private Animator animator;
	private Rigidbody2D rigidbody2D;


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
		animator.CrossFade("Idle", 0.1f);
		playerController.isJump = false;
	}
	public override void OnStayState()
	{
		if (playerController.isLand)
		{
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = Vector3.zero;
		}
		else
		{
			rigidbody2D.gravityScale = playerController.originGravity;
		}

		if (playerController.isDie)
		{
			StateMachine.ChangeState("Die");
		}
		else if (playerController.isJump)
		{
			StateMachine.ChangeState("Jump");
		}
		else if (playerController.direction != 0)
		{
			StateMachine.ChangeState("Run");
		}
	}
}